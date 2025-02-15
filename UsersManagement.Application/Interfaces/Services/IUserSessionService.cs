using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Interfaces.Services;

public interface IUserSessionService
{
    Task<List<UserSessionInfoDto>>  GetAllEmployeeSessions();
    Task<DeleteUserSessionResponseDto> DeleteUserSession(string userSessionId);
}