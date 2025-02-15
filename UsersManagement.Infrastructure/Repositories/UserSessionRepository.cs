using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Enums;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure.Repositories;

public class UserSessionRepository : GenericRepository<UserSession>, IUserSessionRepository 
{
    public UserSessionRepository(UserManagementDbContext context): base(context)
    {
    }

    public async Task<List<UserSessionInfoDto>> GetAllEmployeeSession()
    {
        return await _context
            .UserSession
            .Include(us=>us.UserAccount)
            .Where(p=>p.UserAccount.UserRoleId == (int)UserRoleEnum.Employee)
            .Select(p=>new UserSessionInfoDto
            {
                Email = p.UserAccount.Email,
                CreatedAt = p.CreatedAt,
                ExpiredAt = p.ExpireAt,
                DeviceInfo = p.DeviceInfo,
                SessionId = p.SessionId.ToString(),
                UserId = p.UserId.ToString(),
            }).ToListAsync();
    }

    public Task<UserSession?> GetUserSessionBySid(string sessionId)
    {
        return _context
            .UserSession
            .FirstOrDefaultAsync(p=>p.SessionId.ToString() == sessionId);
    }
}