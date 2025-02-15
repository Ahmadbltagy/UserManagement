using MediatR;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserSessionHandler : IRequestHandler<GetUserSessionQuery, List<UserSessionInfoDto>>
{
    private readonly IUserSessionRepository _userSessionRepository;

    public GetUserSessionHandler(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }

    public async Task<List<UserSessionInfoDto>> Handle(GetUserSessionQuery request, CancellationToken cancellationToken)
    {
        return await _userSessionRepository.GetAllEmployeeSession();
    }
}