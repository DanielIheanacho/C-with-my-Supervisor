using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class UserService
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
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToList();

            return (users.Count, users);
        }

        public static int GetUser<T>(T user) where T : User, new()
        {
            using var context = new AppDbContext();
            int userId = context.Users
                .AsNoTracking()
                .Where(u => u.Email == user.Email && u.PassWord == user.PassWord)
                .Select(u => u.UserId)
                .Single();
            return userId;
        }

        public static void UpdateUser<T>(T user, UserInfo userInfo) where T : User, new()
        {
            int userId = GetUser<T>(user);

            using var context = new AppDbContext();

            var userToUpdate = context.Users.Single(u => u.UserId == userId);

            userToUpdate.FirstName = userInfo.FirstName;
            userToUpdate.LastName = userInfo.LastName;
            userToUpdate.Email = userInfo.Email;
            userToUpdate.PassWord = userInfo.PassWord;

            context.SaveChanges();
        }

    }
}