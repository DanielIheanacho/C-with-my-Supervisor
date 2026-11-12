using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Service.Service
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
