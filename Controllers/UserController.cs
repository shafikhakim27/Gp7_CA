using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gp7_CA.Models;
using Gp7_CA.Repository;

namespace Gp7_CA.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new UserRepository();
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        // MVC View action
        public IActionResult Index()
        {
            return View();
        }

        // API endpoints with explicit routes for Swagger
        /// <summary>
        /// Authenticates the user and returns user details
        /// </summary>
        /// <param name="user">The user credentials</param>
        /// <returns>Authentication result</returns>
        [HttpPost]
        [Route("User/Authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Updates the user's completion time
        /// </summary>
        /// <param name="request">The request containing user ID and new completion time</param>
        /// <returns>Update result</returns>
        [HttpPost]
        [Route("User/UpdateCompletionTime")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public IActionResult UpdateCompletionTime([FromBody] CompletionTimeRequest request)
        {
            _logger.LogInformation($"UpdateCompletionTime called: userId={request?.userId}, completionTime={request?.completionTime}");

            if (request == null)
            {
                _logger.LogWarning("UpdateCompletionTime: Request body is null");
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request. Request body cannot be null."
                });
            }

            if (request.userId <= 0)
            {
                _logger.LogWarning($"UpdateCompletionTime: Invalid userId={request.userId}");
                return BadRequest(new
                {
                    success = false,
                    message = $"Invalid userId: {request.userId}. Must be greater than 0."
                });
            }

            if (request.completionTime < 0)
            {
                _logger.LogWarning($"UpdateCompletionTime: Invalid completionTime={request.completionTime}");
                return BadRequest(new
                {
                    success = false,
                    message = $"Invalid completionTime: {request.completionTime}. Must be non-negative."
                });
            }

            try
            {
                bool isUpdated = _repository.UpdateCompletionTime(request.userId, request.completionTime);

                if (isUpdated)
                {
                    _logger.LogInformation($"? Successfully updated userId={request.userId} with completionTime={request.completionTime}");
                    return Ok(new
                    {
                        success = true,
                        message = "Completion time updated successfully",
                        userId = request.userId,
                        completionTime = request.completionTime
                    });
                }

                _logger.LogWarning($"UpdateCompletionTime: User not found for userId={request.userId}");
                return NotFound(new
                {
                    success = false,
                    message = $"User with ID {request.userId} not found"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"? Error updating completion time: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Server error: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves the leaderboard
        /// </summary>
        /// <param name="limit">The number of top users to return</param>
        /// <returns>Leaderboard data</returns>
        [HttpGet]
        [Route("User/Leaderboard")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult Leaderboard([FromQuery] int limit = 5)
        {
            try
            {
                limit = Math.Max(1, Math.Min(limit, 100));
                
                var leaderboard = _repository.GetLeaderboard(limit);
                
                _logger.LogInformation($"Leaderboard: Returned {leaderboard.Count} users");
                
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
            catch (Exception ex)
            {
                _logger.LogError($"Error getting leaderboard: {ex.Message}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Server error: " + ex.Message
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    /// <summary>
    /// Request model for updating user completion time
    /// </summary>
    public class CompletionTimeRequest
    {
        /// <summary>
        /// User ID
        /// </summary>
        /// <example>1</example>
        public int userId { get; set; }
        
        /// <summary>
        /// Completion time in seconds
        /// </summary>
        /// <example>45.5</example>
        public double completionTime { get; set; }
    }
}
