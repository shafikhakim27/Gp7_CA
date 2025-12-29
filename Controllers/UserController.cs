using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gp7_CA.Models;
using Gp7_CA.Repository;
namespace Gp7_CA.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new UserRepository();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] User user)
        {
            var userDetails = _repository.AuthenticateUser(user.username, user.password);

            if (userDetails != null) // Return 200 Ok status if user is valid  
                return Ok(new
                {
                    success = true,
                    message = $"Welcome {userDetails.username}",
                    isPaidUser = $"{userDetails.isPaidUser}"
                });

            // Return 401 Unauthorized if user is not valid
            return Unauthorized(new
            {
                success = false,
                message = "Invalid username or password"
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
