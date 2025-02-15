using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserSessionBySidHandler : IRequestHandler<GetUserSessionBySidQuery, UserSession?>
{
    private readonly IUserSessionRepository _userSessionRepository;

    public GetUserSessionBySidHandler(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }

    public async Task<UserSession?> Handle(GetUserSessionBySidQuery request, CancellationToken cancellationToken)
    {
        return await _userSessionRepository.GetUserSessionBySid(request.SessionId);
    }
}