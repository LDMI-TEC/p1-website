using Microsoft.AspNetCore.Mvc;

namespace poke_poke.Controllers
{
    [Route("/poke")]
    public class PokeController : Controller
    {
        // IWebHostEnviroment is the path to the wwwroot folder
        private readonly IWebHostEnvironment _env;

        public PokeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult PokeGame()
        {
            var filePath = Path.Combine(_env.WebRootPath, "poke.html");
            return PhysicalFile(filePath, "text/html");
        }

        [HttpGet("leaderboard")]
        public IActionResult PokeLeaderboard()
        {
            var filePath = Path.Combine(_env.WebRootPath, "leaderboard.html");
            return PhysicalFile(filePath, "text/html");
        }
    }
}