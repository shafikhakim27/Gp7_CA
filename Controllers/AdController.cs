using System.Diagnostics;
using Gp7_CA.Models;
using Microsoft.AspNetCore.Mvc;
namespace Gp7_CA.Controllers
{
    [Route("api/[controller]")]  // define prefix: api/ads
    [ApiController]
    public class AdsController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AdsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("get-next-ad")]
        public IActionResult GetNextAd()
        {
            var adsPath = Path.Combine(_env.WebRootPath, "ads");
            var files = Directory.GetFiles(adsPath, "*.*");

            if (files.Length == 0) return NotFound();

            var randomFile = files[new Random().Next(files.Length)];
            var imageBytes = System.IO.File.ReadAllBytes(randomFile);

            return File(imageBytes, "image/jpeg");
        }
    }

}
