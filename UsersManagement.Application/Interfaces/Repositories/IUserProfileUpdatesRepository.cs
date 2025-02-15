using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IUserProfileUpdatesRepository : IGenericRepository<UserProfileUpdates>
{
    Task<(List<UserProfileUpdatesResponseDto>,int)> GetAllPendingUpdates(int pageNumber, int pageSize);
}