using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class AuthenticationService
    {
        public static void RegisterSchoolAndAdmin(SchoolInfo schoolInfo, UserInfo userInfo)
        {
            School school = School.RegisterSchool(schoolInfo);
            User user = Admin.RegisterNewAdmin(school, userInfo);
            using (var context = new AppDbContext())
            {
                context.Add(school);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public static User? LoginUser(string email, string password)
        {
            using var context = new AppDbContext();

            var user = context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email && u.PassWord == password);

            return user;
        }
    }
}
