using SchoolManagementSystem.Services.SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagement.Models.EnumModels;

namespace SchoolManagementSystem.Services
{
    public class AdminService
    {
        public static void SaveUser<T>(T user) where T : User
        {

            using var context = new AppDbContext();
            context.Add(user);
            context.SaveChanges();
        }

        public static void RemoveUser(int userId)
        {

            using var context = new AppDbContext();

            var user = context.Users.Single(u => u.UserId == userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public static (int Count, List<UserInfo> List) GetUsersInCategory(Admin admin, Roles role)
        {
            using var context = new AppDbContext();

            List<UserInfo> users = context.Users
                .AsNoTracking()
                .Where(u => u.SchoolId == admin.SchoolId && u.Role == role)
                .OrderBy(u => u.LastName)
                .Select(u => new UserInfo
                {   UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName 
                })
                .ToList();

            return (users.Count, users);
        }
    }
}