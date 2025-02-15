using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using UsersManagement.Application.Interfaces.Services;

namespace UsersManagement.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string? GetUserSessionId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Sid)?.Value;
    }

    public string? GetUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
    }

    public string? DeviceInfo()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
    }
}