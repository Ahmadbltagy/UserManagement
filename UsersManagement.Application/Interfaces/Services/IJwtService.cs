namespace UsersManagement.Application.Interfaces.Services;

public interface IJwtService
{
    string? GetUserId();
    string? GetUserSessionId();
    string? GetUserRole();
    string? DeviceInfo();
}