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
            if (user == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request. Please provide username and password."
                });
            }

            var userDetails = _repository.AuthenticateUser(user.username, user.password);

            if (userDetails != null)
                return Ok(new
                {
                    success = true,
                    message = $"Welcome {userDetails.username}",
                    userId = userDetails.id,
                    isPaidUser = userDetails.isPaidUser,
                    completionTime = userDetails.completionTime
                });

            return Unauthorized(new
            {
                success = false,
                message = "Invalid username or password"
            });
        }

        [HttpPost("UpdateCompletionTime")]
        public IActionResult UpdateCompletionTime([FromBody] CompletionTimeRequest request)
        {
            if (request == null || request.userId <= 0 || request.completionTime <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request. Provide valid userId and completionTime."
                });
            }

            bool isUpdated = _repository.UpdateCompletionTime(request.userId, request.completionTime);

            if (isUpdated)
                return Ok(new
                {
                    success = true,
                    message = "Completion time updated successfully"
                });

            return NotFound(new
            {
                success = false,
                message = "User not found"
            });
        }

        [HttpGet("Leaderboard")]
        public IActionResult GetLeaderboard([FromQuery] int limit = 10)
        {
            var leaderboard = _repository.GetLeaderboard(limit);

            return Ok(new
            {
                success = true,
                count = leaderboard.Count,
                leaderboard = leaderboard.Select(u => new
                {
                    username = u.username,
                    completionTime = u.completionTime,
                    isPaidUser = u.isPaidUser
                })
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class CompletionTimeRequest
    {
        public int userId { get; set; }
        public double completionTime { get; set; }
    }
}
