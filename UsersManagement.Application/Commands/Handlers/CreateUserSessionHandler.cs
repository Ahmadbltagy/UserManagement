using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands.Handlers;

public class CreateUserSessionHandler : IRequestHandler<CreateUserSessionCommand, bool>
{
    
    private IUserSessionRepository _userSessionRepository;
    private IJwtService _jwtService;
    public CreateUserSessionHandler(IUserSessionRepository userSessionRepository, IJwtService jwtService)
    {
        _userSessionRepository = userSessionRepository;
        _jwtService = jwtService;
    }

    public async Task<bool> Handle(CreateUserSessionCommand request, CancellationToken cancellationToken)
    {
        
        var userSession = new UserSession
        {
            
            Token = request.UserSession.Token,
            CreatedAt = request.UserSession.CreatedAt,
            ExpireAt = request.UserSession.ExpireAt,
            SessionId = request.UserSession.SessionId,
            UserId = request.UserSession.UserId,
            UserAccountId = request.UserSession.UserAccountId,
            DeviceInfo = _jwtService.DeviceInfo()
        };

        await _userSessionRepository.AddAsync(userSession);
        return await _userSessionRepository.SaveChanagesAsync();
    }
}