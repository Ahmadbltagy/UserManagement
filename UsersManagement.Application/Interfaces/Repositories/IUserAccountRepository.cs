using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IUserAccountRepository : IGenericRepository<UserAccount>
{
    Task<UserAccount?> GetByEmail(string email);
    Task<bool> IsEmailExists(string email);
    Task<List<UserAccount>> GetUserAccountsForSuspend(DateTime thresholdDate);
    Task<(List<UserAccountWithProfileDto>,int)> GetUserAccountWithProfile(int pageNumber, int pageSize, bool IsActive = true);
}