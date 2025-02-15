using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserForSuspendHandler : IRequestHandler<GetUserForSuspendQuery, List<UserAccount>>
{
    private readonly IUserAccountRepository _userAccountRepository;

    public GetUserForSuspendHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public Task<List<UserAccount>> Handle(GetUserForSuspendQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}