using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class SuspendInActiveUserAccountHandler : IRequestHandler<SuspendInActiveUserAccountCommand, bool>
{
    private readonly IUserAccountRepository _userAccountRepository;

    public SuspendInActiveUserAccountHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public async Task<bool> Handle(SuspendInActiveUserAccountCommand request, CancellationToken cancellationToken)
    {
        foreach (var user in request.UserAccounts)
        {
            user.IsActive = false;
        }

        return await _userAccountRepository.SaveChanagesAsync();
    }
}