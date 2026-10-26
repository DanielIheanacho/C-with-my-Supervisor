namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
