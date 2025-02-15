using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IUserProfileRepository : IGenericRepository<UserProfile>
{
    Task<UserProfile?> GetByUserId(string userId);
}