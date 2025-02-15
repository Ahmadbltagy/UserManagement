using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class DeleteUserSessionHandler : IRequestHandler<DeleteUserSessionCommand, bool>
{
    private IUserSessionRepository _userSessionRepository;

    public DeleteUserSessionHandler(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }

    public async Task<bool> Handle(DeleteUserSessionCommand request, CancellationToken cancellationToken)
    {
        _userSessionRepository.Delete(request.UserSession);
        return await _userSessionRepository.SaveChanagesAsync();
    }
}