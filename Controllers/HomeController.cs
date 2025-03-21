using Microsoft.AspNetCore.Mvc;

namespace poke_poke.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        // IWebHostEnviroment is the path to the wwwroot folder
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var filePath = Path.Combine(_env.WebRootPath, "index.html");
            return PhysicalFile(filePath, "text/html");
        }
    }
}