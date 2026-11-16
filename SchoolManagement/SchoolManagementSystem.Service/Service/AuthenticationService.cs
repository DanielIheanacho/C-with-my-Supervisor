using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.DTOS;
using SchoolManagementSystem.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolManagementSystem.Service.Service
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IConfiguration _configuration;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {

            _unitOfWork = unitOfWork;
            //_configuration = configuration;

            _issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer is missing from configuration");
            _audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience is missing from configuration");
            _key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing from configuration");
        }



        public async Task RegisterSchoolAndAdminAsync(SchoolInfo schoolInfo, NewUser userInfo)
        {
            School school = School.RegisterSchool(schoolInfo);
            User user = Admin.RegisterFirstAdmin(school.SchoolId, userInfo);

            await _unitOfWork.Schools.AddAsync(school);
            await _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<LoginResponse?> LoginUserAsync(string email, string password)
        {
            var user = await _unitOfWork.LoginUserAsync(email, password);

            if (user == null)
            {
                return null;

            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

            var secretBytes = Encoding.UTF8.GetBytes(_key);

            var securityKey = new SymmetricSecurityKey(secretBytes);

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                        issuer: _issuer,
                        audience: _audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(5),
                        signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                AccessToken = tokenString,
                ExpiresAt = token.ValidTo
            };
        }
    }
}
