using System.Linq.Expressions;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IGenericRepository <T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T item);
    bool Update(T item);
    bool Delete(T item);
    Task<bool> SaveChanagesAsync();

    
}