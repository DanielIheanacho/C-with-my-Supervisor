using Microsoft.AspNetCore.Identity;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.DTOS;
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

        public async Task CreateUserAsync(int adminId, NewUser newUser, string role)
        {
            Admin? admin = await GetByIdAsync(adminId) as Admin;

            if (admin == null) 
                return;

            var user = admin.RegisterNewUser(newUser, role);
            
            var hasher = new PasswordHasher<User>();
            user.PassWord = hasher.HashPassword(user, user.PassWord);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

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

        }

        public async Task<List<NameAndIdDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users
                .GetAllProjectedAsync(
                u => true,
                u => new NameAndIdDto
                {
                    Id = u.UserId,
                    Name = u.FirstName + " " + u.LastName,
                    Role = u.Role.ToString()
                });

            return users;
        }

        public async Task<List<NameAndIdDto>> GetUsersAsync(int schoolId)
        {
            var users = await _unitOfWork.Users
                .GetAllProjectedAsync(
                u => u.SchoolId == schoolId,
                u => new NameAndIdDto
                {
                    Id = u.UserId,
                    Name = u.FirstName + " " + u.LastName,
                    Role = u.Role.ToString()
                });
            return users;
        }
        public async Task<List<NameAndIdDto>> GetUsersInCategoryAsync(Admin admin, Roles role)
        {
            var users = await _unitOfWork.Users
                .GetAllProjectedAsync(
                u => u.SchoolId == admin.SchoolId & u.Role == role ,
                u => new NameAndIdDto
                {
                    Id = u.UserId,
                    Name = u.FirstName + " " + u.LastName,
                    Role = u.Role.ToString()
    
                });
            return users;
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

           return user;
        }

        public async Task<bool> UpdateUserAsync(int userId, UpdateUser userInfo)
        {
            var userToUpdate = await _unitOfWork.Users.GetByIdAsync(userId);

            if (userToUpdate == null || userInfo == null)
            {
                return false;
            }

            userToUpdate.FirstName = string.IsNullOrWhiteSpace(userInfo.FirstName)
                ? userToUpdate.FirstName
                : userInfo.FirstName;
            userToUpdate.LastName = string.IsNullOrWhiteSpace(userInfo.LastName)
                ? userToUpdate.LastName
                : userInfo.LastName;
            userToUpdate.Email = string.IsNullOrWhiteSpace(userInfo.Email)
                ? userToUpdate.Email
                : userInfo.Email;
            userToUpdate.PassWord = string.IsNullOrWhiteSpace(userInfo.PassWord)
                ? userToUpdate.PassWord
                : userInfo.PassWord;

            var hasher = new PasswordHasher<User>();
            userToUpdate.PassWord = hasher.HashPassword(userToUpdate, userToUpdate.PassWord);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}