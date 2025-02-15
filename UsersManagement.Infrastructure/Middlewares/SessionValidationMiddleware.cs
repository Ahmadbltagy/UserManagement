using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Persistence.DbContext;

public class SessionValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SessionValidationMiddleware> _logger;
    public SessionValidationMiddleware(RequestDelegate next, ILogger<SessionValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, UserManagementDbContext dbContext)
    {
        var path = context.Request.Path.Value?.ToLower();
        if (path == "/api/login")
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Token is missing.");
            return;
        }

        var sessionId = GetSessionIdFromToken(token);
        if (sessionId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid token.");
            return;
        }

        var sessionExists = await dbContext.UserSession
            .AnyAsync(s => s.SessionId.ToString() == sessionId );

        if (!sessionExists)
        {
            _logger.LogWarning($"Invalid or revoked session: {sessionId}");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Session expired or revoked.");
            return;
        }

        await _next(context);
    }

    private string GetSessionIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
        return jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
    }
}
