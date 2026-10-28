using SchoolManagement.Models.ClassModels;

namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<School> Schools { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<ClassRoom> ClassRooms { get; }
        IRepository<Student> Students { get; }

        Task<int> SaveChangesAsync();

        Task<User?> LoginUserAsync(string email, string password);
    }
}
