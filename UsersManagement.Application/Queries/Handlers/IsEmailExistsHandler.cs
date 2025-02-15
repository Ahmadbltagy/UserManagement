using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Queries.Handlers;

public class IsEmailExistsHandler : IRequestHandler<IsEmailExistsQuery, bool>
{
    private IUserAccountRepository _userAccountRepository;

    public IsEmailExistsHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public Task<bool> Handle(IsEmailExistsQuery request, CancellationToken cancellationToken)
    {
        return _userAccountRepository.IsEmailExists(request.Email);
    }
}