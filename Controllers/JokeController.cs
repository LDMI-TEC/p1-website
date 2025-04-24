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
            public async Task<ActionResult<IEnumerable<Joke>>> GetJokes()
            {
                return Ok(
                    await _context.jokes
                    .Include(j => j.author)
                    .Include(j => j.category)
                    .Where(j => j.isApproved)
                    .ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Joke>> GetJoke(int id)
            {
                var joke = await _context.jokes
                    .Include(j => j.author)
                    .Include(j => j.category)
                    .FirstOrDefaultAsync(j => j.id == id);

                if (joke == null) {
                    return NotFound();
                }
                
                return Ok(joke);
            }

            [HttpGet("category/{catId}")]
            public async Task<ActionResult<IEnumerable<Joke>>> GetJokesByCategory(int catId)
            {
                var jokes = await _context.jokes
                    .Include(j => j.author)
                    .Include(j => j.category)
                    .Where(j => j.categoryId == catId && j.isApproved)
                    .ToListAsync();

                if (jokes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jokes);
            }

            [HttpGet("author/{aId}")]
            public async Task<ActionResult<IEnumerable<Joke>>> GetJokesByAuthor(int aId)
            {
                var jokes = await _context.jokes
                    .Include(j => j.author)
                    .Include(j => j.category)
                    .Where(j => j.authorId == aId && j.isApproved)
                    .ToListAsync();

                if (jokes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jokes);
            }

            [HttpPost]
            public async Task<ActionResult> PostJoke(Joke joke)
            {
                joke.createAt = DateTime.Now;
                joke.likes = 0;
                joke.dislikes = 0;
                joke.isApproved = false;

                _context.jokes.Add(joke);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetJoke), new { id = joke.id }, joke);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> PutJoke(int id, Joke joke)
            {
                if (id != joke.id)
                {
                    return BadRequest();
                }

                _context.Entry(joke).State = EntityState.Modified;

                // Don't allow the user to modify some properties createdAt, likes and dislikes
                _context.Entry(joke).Property(x => x.createAt).IsModified = false;
                _context.Entry(joke).Property(x => x.likes).IsModified = false;
                _context.Entry(joke).Property(x => x.dislikes).IsModified = false;

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

                joke.likes++;
                await _context.SaveChangesAsync();

                return Ok(new { likes = joke.likes, dislikes = joke.dislikes });
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

                joke.dislikes++;
                await _context.SaveChangesAsync();

                return Ok(new { likes = joke.likes, dislikes = joke.dislikes });
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
                return _context.jokes.Any(j => j.id == id);
            }
        }
}