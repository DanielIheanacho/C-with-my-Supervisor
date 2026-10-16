using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class AuthenticationService
    {
        public static async Task RegisterSchoolAndAdminAsync(SchoolInfo schoolInfo, UserInfo userInfo)
        {
            School school = School.RegisterSchool(schoolInfo);
            User user = Admin.RegisterNewAdmin(school, userInfo);
            using (var context = new AppDbContext())
            {
                context.Add(school);
                context.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public static async Task<User?> LoginUserAsync(string email, string password)
        {
            using var context = new AppDbContext();

            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.PassWord == password);

            return user;
        }
    }
}
