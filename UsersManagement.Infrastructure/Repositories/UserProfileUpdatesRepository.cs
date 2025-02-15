using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class UserProfileUpdatesRepository : GenericRepository<UserProfileUpdates>, IUserProfileUpdatesRepository
{

    public UserProfileUpdatesRepository(UserManagementDbContext userManagementDbContext) : base(userManagementDbContext)
    {
        
    }


    public async Task<(List<UserProfileUpdatesResponseDto>,int)> GetAllPendingUpdates(int pageNumber, int pageSize)
    {
        int skipCounter = pageSize * (pageNumber - 1);
        
        var data =  await _context
            .UserProfileUpdate
            .Skip(skipCounter)
            .Take(pageSize)
            .Where(p => !p.IsApproved)
            .Select(p=>new UserProfileUpdatesResponseDto()
            {
                RequestId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                InsertionDate = p.InsertionDate,
                UserId = p.UserId,
                DateOfBirth = p.DateOfBirth,
            })
            .ToListAsync();
        
        var totalCount = await _context.UserProfileUpdate.CountAsync(p=>!p.IsApproved);
        
        return (data, totalCount);

    }
}