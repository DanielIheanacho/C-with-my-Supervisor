using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service.Service
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork; 

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterSchoolAndAdminAsync(SchoolInfo schoolInfo, UserInfo userInfo)
        {
            School school = School.RegisterSchool(schoolInfo);
            User user = Admin.RegisterNewAdmin(school, userInfo);

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
