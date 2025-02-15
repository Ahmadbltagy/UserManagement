using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserAccountByEmailHandler : IRequestHandler<GetUserAccountByEmailQuery, UserAccount?>
{
    private IUserAccountRepository _userAccountRepository;
    
    public GetUserAccountByEmailHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }
    
    public Task<UserAccount?> Handle(GetUserAccountByEmailQuery request, CancellationToken cancellationToken)
    {
        return _userAccountRepository.GetByEmail(request.Email);
    }
}