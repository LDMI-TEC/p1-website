using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poke_poke.Models.Horoscopes;
using poke_poke.Repository;

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
    }
}