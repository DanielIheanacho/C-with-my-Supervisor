using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Service;
using System.Security.Claims;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private int CurrentUserId =>
     int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private int CurrentSchoolId =>
            int.Parse(User.FindFirstValue("SchoolId")!);
        {
            _userService = userservice;
        }

        private bool IsCurrentUser(int callerId)
        {
            var userIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdText!);

            return (callerId == userId);
        
        }

        [HttpPost("users/{adminId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(int adminId, NewUser user)
        {
            await _userService.CreateUserAsync(adminId, user, user.Role);
            return Ok();
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(int id)
        {
            return Ok(await _userService.GetUsersAsync(id));
        }

        [HttpGet("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPut("users/{id}")]
        [Authorize(Roles = "Admin,Teacher,Guardian")]
        public async Task<IActionResult> UpdateUserAsync(int id, UpdateUser user)
        {
            if (!IsCurrentUser(id))
            {
                return Forbid();
            }

            if (user == null)
            {
                return BadRequest("Request body cannot be empty.");
            }

            var flag = await _userService.UpdateUserAsync(id, user);

            if (!flag)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpDelete("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
