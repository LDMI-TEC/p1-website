using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using poke_poke.Models.Jokes;
using poke_poke.Repository;

namespace poke_poke.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class JokeController : ControllerBase
        {
            private readonly JokeAppContext _context;

            public JokeController(JokeAppContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<JokeDto>>> GetJokes()
            {
                var jokes = await _context.jokes
                    .Include(j => j.Author)
                    .Include(j => j.Category)
                    .Where(j => j.IsApproved)
                    .ToListAsync();

                return Ok(jokes.Select(MapJokeToDto));
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<JokeDto>> GetJoke(int id)
            {
                var joke = await _context.jokes
                    .Include(j => j.Author)
                    .Include(j => j.Category)
                    .FirstOrDefaultAsync(j => j.Id == id);

                if (joke == null) {
                    return NotFound();
                }
                
                return Ok(MapJokeToDto(joke));
            }

            [HttpGet("category/{catId}")]
            public async Task<ActionResult<IEnumerable<JokeDto>>> GetJokesByCategory(int catId)
            {
                var jokes = await _context.jokes
                    .Include(j => j.Author)
                    .Include(j => j.Category)
                    .Where(j => j.CategoryId == catId && j.IsApproved)
                    .ToListAsync();

                if (jokes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jokes.Select(MapJokeToDto));
            }

            [HttpGet("author/{aId}")]
            public async Task<ActionResult<IEnumerable<JokeDto>>> GetJokesByAuthor(int aId)
            {
                var jokes = await _context.jokes
                    .Include(j => j.Author)
                    .Include(j => j.Category)
                    .Where(j => j.AuthorId == aId && j.IsApproved)
                    .ToListAsync();

                if (jokes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jokes.Select(MapJokeToDto));
            }

            [HttpPost]
            public async Task<ActionResult> PostJoke(JokeCreateDto jokeDto)
            {
                if (string.IsNullOrEmpty(jokeDto.AuthorName) || string.IsNullOrEmpty(jokeDto.Joke)) 
                {
                    return BadRequest("Author name and joke is required");
                }

                // find if author already exists in the db
                // if not create a new author with the information given in the request
                var author = await _context.authors.FirstOrDefaultAsync(x => x.Name == jokeDto.AuthorName);

                if (author == null)
                {
                    author = new Author
                    {
                        Name = jokeDto.AuthorName,
                        Age = jokeDto.AuthorAge ?? 40
                    };

                    _context.authors.Add(author);
                    await _context.SaveChangesAsync();
                }

                // create joke
                var joke = new Joke
                {
                    AuthorId = author.Id,
                    CategoryId = jokeDto.CategoryId,
                    JokeText = jokeDto.Joke,
                    CreatedAt = DateTime.Now,
                    Likes = 0,
                    Dislikes = 0,
                    IsApproved = false
                };

                _context.Add(joke);
                await _context.SaveChangesAsync();

                await _context.Entry(joke).Reference(j => joke.Author).LoadAsync();
                await _context.Entry(joke).Reference(j => joke.Category).LoadAsync();

                return CreatedAtAction(nameof(GetJoke), new { id = joke.Id }, MapJokeToDto(joke));
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> PutJoke(int id, JokeDto jokeDto)
            {
                if (id != jokeDto.Id)
                {
                    return BadRequest();
                }

                var joke = await _context.jokes.FindAsync(id);
                if (joke == null)
                {
                    return NotFound();
                }

                // update the fields we want to allow updating
                // We can potentially add more in the future if we would like to do so like isApproved
                joke.JokeText = jokeDto.JokeText;

                try 
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!JokeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // increment like by 1 and return object with the jokes likes and dislikes
            [HttpPut("{id}/like")]
            public async Task<IActionResult> LikeJoke(int id)
            {
                var joke = await _context.jokes.FindAsync(id);

                if (joke == null)
                {
                    return NotFound();
                }

                joke.Likes++;
                await _context.SaveChangesAsync();

                return Ok(new JokeLikesDto { Likes = joke.Likes, Dislikes = joke.Dislikes });
            }

            // increment dislikes and return object with the jokes likes and dislikes
            [HttpPut("{id}/dislike")]
            public async Task<IActionResult> DislikeJoke(int id)
            {
                var joke = await _context.jokes.FindAsync(id);

                if (joke == null) 
                {
                    return NotFound();
                }

                joke.Dislikes++;
                await _context.SaveChangesAsync();

                return Ok(new JokeLikesDto { Likes = joke.Likes, Dislikes = joke.Dislikes });
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteJoke(int id)
            {
                var joke = await _context.jokes.FindAsync(id);

                if (joke == null)
                {
                    return NotFound();
                }

                _context.Remove(joke);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            // used internally in this class to see if a joke exists
            private bool JokeExists(int id)
            {
                return _context.jokes.Any(j => j.Id == id);
            }

            // method to map entity to DTO
            private JokeDto MapJokeToDto(Joke joke)
            {
                return new JokeDto
                {
                    Id = joke.Id,
                    JokeText = joke.JokeText,
                    CreatedAt = joke.CreatedAt,
                    IsApproved = joke.IsApproved,
                    Likes = joke.Likes,
                    Dislikes = joke.Dislikes,
                    Author = joke.Author != null
                        ? new AuthorDto
                        {
                            Id = joke.Author.Id,
                            Name = joke.Author.Name,
                            Age = joke.Author.Age,
                        } : null,
                    Category = joke.Category != null
                        ? new CategoryDto
                        {
                            Id = joke.Category.Id,
                            Name = joke.Category.Name
                        } : null
                };
            }
        }
}