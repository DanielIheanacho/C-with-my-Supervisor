using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IRepository<User> _users;
        private IRepository<School> _schools;
        private IRepository<Subject> _subjects;
        private IRepository<ClassRoom> _classRooms;
        private IRepository<Student> _students;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _users ??= new Repository<User>(_context);
        public IRepository<School> Schools => _schools ??= new Repository<School>(_context);
        public IRepository<Subject> Subjects => _subjects ??= new Repository<Subject>(_context);
        public IRepository<ClassRoom> ClassRooms => _classRooms ??= new Repository<ClassRoom>(_context);

        public IRepository<Student> Students => _students ??= new Repository<Student>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {


            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.PassWord == password);

            return user;
        }
    }
}