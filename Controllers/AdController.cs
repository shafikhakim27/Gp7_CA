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
        public IActionResult GetNextAd([FromQuery] string lastAd = "")
        {
            var adsPath = Path.Combine(_env.WebRootPath, "ads");
            // get all files
            var allFiles = Directory.GetFiles(adsPath, "*.*");
    
            if (allFiles.Length == 0) return NotFound();
            if (allFiles.Length == 1)
            {
                // if only have one ads picture
                return File(System.IO.File.ReadAllBytes(allFiles[0]), "image/jpeg");
            }
    
            // filter the last ad's path, so that the same ad won't show the 2nd time after 30s
            var availableFiles = allFiles.Where(f => Path.GetFileName(f) != lastAd).ToList();
    
            // randomly choose another ad
            var randomFile = availableFiles[new Random().Next(availableFiles.Count)];
    
            // return the file name to frontend in Header
            Response.Headers.Add("Current-Ad-Name", Path.GetFileName(randomFile));
    
            var imageBytes = System.IO.File.ReadAllBytes(randomFile);
            return File(imageBytes, "image/jpeg");
        }
    }

    //     [HttpGet("get-next-ad")]
    //     public IActionResult GetNextAd()
    //     {
    //         var adsPath = Path.Combine(_env.WebRootPath, "ads");
    //         var files = Directory.GetFiles(adsPath, "*.*");

    //         if (files.Length == 0) return NotFound();

    //         var randomFile = files[new Random().Next(files.Length)];
    //         var imageBytes = System.IO.File.ReadAllBytes(randomFile);

    //         return File(imageBytes, "image/jpeg");
    //     }
    

}
