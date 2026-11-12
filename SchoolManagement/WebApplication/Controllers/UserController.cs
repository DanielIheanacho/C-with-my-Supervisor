using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Service;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userservice)
        {
            _userService = userservice;
        }

        [HttpPost("users/{adminId}")]
        public async Task<IActionResult> CreateUser(int adminId, NewUser user)
        {
            await _userService.CreateUserAsync(adminId, user, user.Role);
            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(int id)
        {
            return Ok(await _userService.GetUsersAsync(id));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, UpdateUser user)
        {
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
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
