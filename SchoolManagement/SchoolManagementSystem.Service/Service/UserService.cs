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
                    Name = u.FirstName + " " + u.LastName
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
                    Name = u.FirstName + " " + u.LastName
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
                    Name = u.FirstName + " " + u.LastName
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
            userToUpdate.FirstName = userInfo.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = userInfo.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = userInfo.Email ?? userToUpdate.Email;
            userToUpdate.PassWord = userInfo.PassWord ?? userToUpdate.PassWord;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}