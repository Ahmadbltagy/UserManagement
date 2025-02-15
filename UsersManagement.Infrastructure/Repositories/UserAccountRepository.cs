using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class UserAccountRepository :  GenericRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(UserManagementDbContext userManagementDbContext) : base(userManagementDbContext)
    {
        
    }
    
    public async Task<UserAccount?> GetByEmail(string email)
    {
        return await _context
            .UserAccount
            .Include(u => u.UserRole)
            .Include(p=>p.UserProfile)
            .FirstOrDefaultAsync(
                    x => 
                    x.Email == email.ToLower() &&
                    x.IsActive
                );
    }

    public async Task<bool> IsEmailExists(string email)
    {
        return await _context.UserAccount
            .AnyAsync(p=>p.Email == email.ToLower());
    }

    public async Task<List<UserAccount>> GetUserAccountsForSuspend(DateTime thresholdDate)
    {
        return await _context
            .UserAccount
            .Where(u => u.LastLoginAt != null && u.LastLoginAt < thresholdDate && u.IsActive)
            .ToListAsync();
    }

    public async Task<(List<UserAccountWithProfileDto>, int)> GetUserAccountWithProfile(int pageNumber, int pageSize, bool isActive)
    {
        var data =  await _context.UserAccount
            .Where(x=>x.IsActive == isActive)
            .Include(p=>p.UserProfile)
            .Include(p=>p.UserRole)
            .Skip((pageNumber-1) * pageSize)
            .Take(pageSize)
            .Select(p=>new UserAccountWithProfileDto
            {
                Email = p.Email,
                UserRole = p.UserRole.Name,
                FirstName = p.UserProfile.FirstName,
                LastName = p.UserProfile.LastName,
                UserId = p.UserId.ToString(),
                DateOfBirth = p.UserProfile.DateOfBirth,
                
            })
            .ToListAsync();
        var totalCount = await _context.UserAccount.CountAsync(p=>p.IsActive == isActive);
        
        return (data,totalCount);
    }
}