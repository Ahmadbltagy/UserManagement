using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class UserProfilePendingUpdatesRepository : GenericRepository<UserProfilePendingUpdates>, IUserProfilePendingUpdatesRepository
{

    public UserProfilePendingUpdatesRepository(UserManagementDbContext userManagementDbContext) : base(userManagementDbContext)
    {
        
    }


    public async Task<(List<UserProfilePendingUpdatesResponseDto>,int)> GetAllPendingUpdates(int pageNumber, int pageSize)
    {
        int skipCounter = pageSize * (pageNumber - 1);
        
        var data =  await _context
            .UserProfilePendingUpdate
            .Skip(skipCounter)
            .Take(pageSize)
            .Where(p => !p.IsApproved)
            .Select(p=>new UserProfilePendingUpdatesResponseDto()
            {
                RequestId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                InsertionDate = p.InsertionDate,
                UserId = p.UserId,
                DateOfBirth = p.DateOfBirth,
            })
            .ToListAsync();
        
        var totalCount = await _context.UserProfilePendingUpdate.CountAsync(p=>!p.IsApproved);
        
        return (data, totalCount);

    }
}