using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Service.Service
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveUserAsync<T>(T user) where T : User
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Dispose();
        }

        public async Task DeleteUserAsync(int userId)
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
            {
                return;
            }

            await _unitOfWork.Users.Delete(user!);
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Dispose();
        }

        //public async Task<(int Count, List<UserInfo> List)> GetUsersInCategoryAsync(Admin admin, Roles role)
        //{
        //    using var context = new AppDbContext();

        //    List<UserInfo> users = await context.Users
        //        .AsNoTracking()
        //        .Where(u => u.SchoolId == admin.SchoolId && u.Role == role)
        //        .OrderBy(u => u.LastName)
        //        .Select(u => new UserInfo
        //        {
        //            UserId = u.UserId,
        //            FirstName = u.FirstName,
        //            LastName = u.LastName
        //        })
        //        .ToListAsync(); 

        //    return (users.Count, users);
        //}

        public async Task<T?> GetByIdAsync<T>(int userId) where T : User, new()
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            _unitOfWork.Dispose();

            return (T)user;
        }

        //public static async Task UpdateUserAsync<T>(T user, UserInfo userInfo) where T : User, new()
        //{
        //    int userId = await GetUserAsync<T>(user);

        //    using var context = new AppDbContext();

        //    var userToUpdate = await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        //    if (userToUpdate == null)
        //    {
        //        return;
        //    }
        //    userToUpdate.FirstName = userInfo.FirstName;
        //    userToUpdate.LastName = userInfo.LastName;
        //    userToUpdate.Email = userInfo.Email;
        //    userToUpdate.PassWord = userInfo.PassWord;

        //    await context.SaveChangesAsync();
        //}

    }
}