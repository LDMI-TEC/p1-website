using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        }
    
}