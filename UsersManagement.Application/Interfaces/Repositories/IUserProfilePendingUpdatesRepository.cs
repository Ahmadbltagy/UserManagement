using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IUserProfilePendingUpdatesRepository : IGenericRepository<UserProfilePendingUpdates>
{
    Task<(List<UserProfilePendingUpdatesResponseDto>,int)> GetAllPendingUpdates(int pageNumber, int pageSize);
}