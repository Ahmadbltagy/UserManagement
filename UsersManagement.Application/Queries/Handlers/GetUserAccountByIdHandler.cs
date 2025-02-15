using MediatR;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries.Handlers;

public class GetUserAccountByIdHandler : IRequestHandler<GetUserAccountByIdQuery, UserAccount?>
{
    private readonly IUserAccountRepository _userAccountRepository;

    public GetUserAccountByIdHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public async Task<UserAccount?> Handle(GetUserAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userAccountRepository.GetByIdAsync(request.UserAccountId);
    }
}