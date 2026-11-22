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
        public UserController(
            UserService userservice,
            IAuthorizationService authorizationService)
        {
            _userService = userservice;
            _authorizationService = authorizationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(NewUser user)
        {
            await _userService.CreateUserAsync(CurrentUserId, user, user.Role);
            return Ok("Done");
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync(CurrentSchoolId));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var targetUser = await _userService.GetByIdAsync(id);

            if (targetUser == null)
            {
                return NotFound("User not Found");
            }

            var adminAuthorizationResult = await _authorizationService.AuthorizeAsync(
                User,
                targetUser,
                "AdminSameSchool");

            var personalAuthorizationResult = await _authorizationService.AuthorizeAsync(
                User,
                targetUser,
                "IsMe");

            if (!adminAuthorizationResult.Succeeded && !
                personalAuthorizationResult.Succeeded)
            {
                return Forbid();
            }


            var userResponse = new NameAndIdDto
            {
                Id = targetUser.UserId,
                Name = targetUser.FirstName + " " + targetUser.LastName,
                Role = targetUser.Role.ToString()
            };

            return Ok(userResponse);
        }

        [HttpPut("Updateusers/{id}")]
        //[Authorize(Roles = "Admin,Teacher,Guardian")]
        public async Task<IActionResult> UpdateUserAsync(
            int id,
            UpdateUser user)
        {
            if (user == null)
            {
                return BadRequest("Request body cannot be empty");
            }

            var targetUser = await _userService.GetByIdAsync(id);

            if (targetUser == null)
            {
                return NotFound("User not found.");
            }

            var adminAuthorizationResult = await _authorizationService.AuthorizeAsync(
                User,
                targetUser,
                "AdminSameSchool");

            var personalAuthorizationResult = await _authorizationService.AuthorizeAsync(
                User,
                targetUser,
                "IsMe");


            if (!adminAuthorizationResult.Succeeded &&
                !personalAuthorizationResult.Succeeded)
            {
                return Forbid();
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
            var targetUser = await _userService.GetByIdAsync(id);

            if (targetUser == null)
            {
                return NotFound("User not Found.");
            }

            var adminAuthorizationResult = await _authorizationService.AuthorizeAsync(
                User,
                targetUser,
                "AdminSameSchool");

            if (!adminAuthorizationResult.Succeeded)                
            {
                return Forbid();
            }

            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
