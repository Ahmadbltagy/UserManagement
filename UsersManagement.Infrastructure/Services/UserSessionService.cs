using MediatR;
using UsersManagement.Application.Commands;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Infrastructure.Services;

public class UserSessionService : IUserSessionService
{
    private readonly IMediator _mediator;

    public UserSessionService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<UserSessionInfoDto>> GetAllEmployeeSessions()
    {
        var query = new GetUserSessionQuery();
        return await _mediator.Send(query);
    }

    public async Task<DeleteUserSessionResponseDto> DeleteUserSession(string userSessionId)
    {
        var userSession = await GetUserSessionBySid(userSessionId);
        
        if (userSession == null)
        {
            return new DeleteUserSessionResponseDto()
            {
                IsSuccess = false,
                Message = "UserSession not found."
            };
        }

        var command = new DeleteUserSessionCommand(userSession);
        var result = await _mediator.Send(command);

        if (result)
        {
            return new DeleteUserSessionResponseDto()
            {
                IsSuccess = true,
                Message = "UserSession deleted."
            };
        }

        return new DeleteUserSessionResponseDto()
        {
            IsSuccess = false,
            Message = "Internal error."
        };
    }
    private async Task<UserSession?> GetUserSessionBySid(string sessionId)
    {
        var query = new GetUserSessionBySidQuery(sessionId);
        return await _mediator.Send(query);
    }
}