using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.ClassModels;
using SchoolManagement.Web.DTOs;
using SchoolManagementSystem.Service.Service;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterSchoolAsync(RegisterSchoolRequest school)
        {
            var schoolInfo = new SchoolInfo
            {
                Name = school.SchoolName
            };
            var userInfo = new NewUser
            {
                FirstName = school.AdminFirstName,
                LastName = school.AdminLastName,
                Email = school.AdminEmail,
                PassWord = school.AdminPassWord,
                Role = null
            };

            await _authenticationService.RegisterSchoolAndAdminAsync(schoolInfo, userInfo);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest login )
        {
             var user = await _authenticationService.LoginUserAsync(login.Email, login.PassWord);
            if (user == null)
            { 
                return Unauthorized("email or password is incorrect."); 
            }

            return Ok(user);

        }
    }
}
