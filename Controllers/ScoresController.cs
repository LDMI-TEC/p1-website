using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poke_poke.Models;
using poke_poke.Repository;


namespace poke_poke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        //private static List<GameScore> scores = new List<GameScore>();
        private readonly GameScoreContext _context;
        // holds the validTokens so you can't just post scores
        private static Dictionary<string, bool> validTokens = new Dictionary<string, bool>();

        public ScoresController(GameScoreContext context)
        {
            _context = context;
        }

        // generates a random token sets valid to true
        [HttpGet("generate-token")]
        public ActionResult<string> GenerateToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            validTokens[token] = true;
            return Ok(token);
        }

        // GET all scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameScore>>> GetScores()
        {
            var scores = await _context.GameScores.ToListAsync();
            return Ok(scores);
        }

        // GET a single score by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<GameScore>> GetScore(long id)
        {
            var gameScore = await _context.GameScores.FindAsync(id);

            if (gameScore == null)
            {
                return NotFound($"No score found with ID {id}");
            }

            return Ok(gameScore);
        }

        // POST a new score (Requires a valid token)
        [HttpPost]
        public async Task<ActionResult<GameScore>> PostScore(GameScoreDTO scoreDTO, [FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token) || !validTokens.ContainsKey(token))
            {
                return Unauthorized("Invalid or missing token");
            }

            // remove the token after use (as it is a one time use token)
            validTokens.Remove(token);

            var gameScore = new GameScore
            {
                PlayerName = scoreDTO.PlayerName,
                Score = scoreDTO.Score,
                TimeOfScore = scoreDTO.TimeOfScore,
            };

            _context.GameScores.Add(gameScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScore), new { id = gameScore.Id }, gameScore);
        }

        // DELETE a score by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScore(long id)
        {
            var score = await _context.GameScores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.GameScores.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}