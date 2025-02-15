using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class UpdateUserAccountLastLoginHandler : IRequestHandler<UpdateUserAccountLastLoginCommand, bool>
{
    private IUserAccountRepository _userAccountRepository;

    public UpdateUserAccountLastLoginHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public Task<bool> Handle(UpdateUserAccountLastLoginCommand request, CancellationToken cancellationToken)
    {
         request.UserAccount.LastLoginAt = DateTime.Now;
        _userAccountRepository.Update(request.UserAccount);
        return _userAccountRepository.SaveChanagesAsync();
    }
}