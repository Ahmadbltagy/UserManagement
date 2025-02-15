using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class UserProfileRepository :  GenericRepository<UserProfile>, IUserProfileRepository 
{
    public UserProfileRepository(UserManagementDbContext userManagementDbContext) : base(userManagementDbContext)
    {
        
    }


    public async Task<UserProfile?> GetByUserId(string userId)
    {
        return await _context
            .UserProfile
            .FirstOrDefaultAsync(p=>p.UserId.ToString() == userId);
    }
}