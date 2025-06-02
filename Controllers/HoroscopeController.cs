using Microsoft.AspNetCore.Mvc;
using poke_poke.Repository;

namespace poke_poke.Controllers
{
    [Route("/horoscope")]
    public class HoroscopeController : Controller
    {
        // IWebHostEnvironment is the path to the wwwroot folder
        private readonly IWebHostEnvironment _env;

        public HoroscopeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Horoscope()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "horoskop.html");
            return PhysicalFile(filePath, "text/html");
        }
    } 
}