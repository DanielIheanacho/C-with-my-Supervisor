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

        public async Task SaveUserAsync<T>(T user) where T : User
        {
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

        public async Task<List<NameAndIdDto>> GetUsersInCategoryAsync(Admin admin)
        {
            var users = await _unitOfWork.Users
                .GetAllProjectedAsync(
                u => u.SchoolId == admin.SchoolId,
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

        public async Task<T?> GetByIdAsync<T>(int userId) where T : User, new()
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return (T)user;
        }

        public async Task UpdateUserAsync(int userId, UserInfo userInfo)
        {
            var userToUpdate = await _unitOfWork.Users.GetByIdAsync(userId);

            if (userToUpdate == null)
            {
                return;
            }
            userToUpdate.FirstName = userInfo.FirstName;
            userToUpdate.LastName = userInfo.LastName;
            userToUpdate.Email = userInfo.Email;
            userToUpdate.PassWord = userInfo.PassWord;

            await _unitOfWork.SaveChangesAsync();
        }

    }
}