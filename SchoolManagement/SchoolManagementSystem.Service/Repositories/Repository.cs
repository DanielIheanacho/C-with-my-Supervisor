using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<TResult>> GetAllProjectedAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public Task Update(T entity)
        {
            _dbSet.Update(entity);

            return Task.CompletedTask;

        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}