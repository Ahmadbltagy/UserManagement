using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Repositories;

public interface IUserSessionRepository : IGenericRepository<UserSession>
{
    Task<List<UserSessionInfoDto>>  GetAllEmployeeSession();
    Task<UserSession?> GetUserSessionBySid(string sessionId);
}