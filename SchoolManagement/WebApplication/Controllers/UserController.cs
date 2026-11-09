using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }
    }
}
