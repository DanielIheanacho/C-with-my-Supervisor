using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class UserService
    {
        public static async Task SaveUserAsync<T>(T user) where T : User
        {

            using var context = new AppDbContext();
            context.Add(user);
            await context.SaveChangesAsync();
        }

        public static async Task RemoveUserAsync(int userId)
        {

            using var context = new AppDbContext();

            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public static async Task<(int Count, List<UserInfo> List)> GetUsersInCategoryAsync(Admin admin, Roles role)
        {
            using var context = new AppDbContext();

            List<UserInfo> users = await context.Users
                .AsNoTracking()
                .Where(u => u.SchoolId == admin.SchoolId && u.Role == role)
                .OrderBy(u => u.LastName)
                .Select(u => new UserInfo
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync(); 

            return (users.Count, users);
        }

        public static async Task<int> GetUserAsync<T>(T user) where T : User, new()
        {
            using var context = new AppDbContext();
            int userId = await context.Users
                .AsNoTracking()
                .Where(u => u.Email == user.Email && u.PassWord == user.PassWord)
                .Select(u => u.UserId)
                .SingleOrDefaultAsync();
            return userId;
        }

        public static async Task UpdateUserAsync<T>(T user, UserInfo userInfo) where T : User, new()
        {
            int userId = await GetUserAsync<T>(user);

            using var context = new AppDbContext();

            var userToUpdate = await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

            if (userToUpdate == null)
            {
                return;
            }
            userToUpdate.FirstName = userInfo.FirstName;
            userToUpdate.LastName = userInfo.LastName;
            userToUpdate.Email = userInfo.Email;
            userToUpdate.PassWord = userInfo.PassWord;

            await context.SaveChangesAsync();
        }

    }
}