using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly UserManagementDbContext _context;
    
    public GenericRepository(UserManagementDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();
        
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return  await _context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T item)
    {
        await _context.Set<T>().AddAsync(item);
    }

    public bool Update(T item)
    {
        _context.Entry<T>(item).State = EntityState.Modified;
        return true;
    }

    public bool Delete(T item)
    {
        _context.Set<T>().Remove(item);
        return true;
    }

    public async Task<bool> SaveChanagesAsync()
    {
      return  await _context.SaveChangesAsync() > 0;
    }
}