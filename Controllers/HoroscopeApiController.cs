using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poke_poke.Models.Horoscopes;
using poke_poke.Repository;
using poke_poke.services;

namespace poke_poke.Controllers
{
    [Route("api/horoscope")]
    [ApiController]
    public class HoroscopeApiController : ControllerBase
    {
        private readonly HoroscopeContext _context;

        public HoroscopeApiController(HoroscopeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horoscope>>> GetHoroscopes()
        {
            try
            {
                var horoscopes = await _context.horoscopes.ToListAsync();
                return Ok(horoscopes);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{zodiac}")]
        public ActionResult<List<string>> GetHoroscopeByZodiac(String zodiac)
        {
            try
            {
                // get the singleton instance (already initialized in program.cs)
                var dataofTheDay = DataOfTheDay.GetInstance();
                var horoscopes = dataofTheDay.GetHoroScope();

                // check f the zodiacs exists in the dictionary (case-insensitive)
                var zodiacKey = horoscopes.Keys.FirstOrDefault(k =>
                    k.Equals(zodiac, StringComparison.OrdinalIgnoreCase));

                if (zodiacKey == null)
                {
                    return NotFound($"No horoscope found for zodiac {zodiac}");
                }

                return Ok(horoscopes[zodiacKey]);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        
        
    }
}