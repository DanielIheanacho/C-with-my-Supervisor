using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class UserService
    {
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