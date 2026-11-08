using System.Linq.Expressions;

namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<TResult>> GetAllProjectedAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector);
        Task<T?> GetByIdAsync(int id);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
