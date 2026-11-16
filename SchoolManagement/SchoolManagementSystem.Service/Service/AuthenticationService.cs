using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;

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
            User user = Admin.RegisterFirstAdmin(school, userInfo);

            await _unitOfWork.Schools.AddAsync(school);
            await _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            var user = await _unitOfWork.LoginUserAsync(email, password);
            return user;
        }

    }
}
