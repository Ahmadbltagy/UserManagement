using MediatR;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class UpdateReactiveSuspendUserHandler : IRequestHandler<UpdateReactiveSuspendUserCommand, bool>
{
    private readonly IUserAccountRepository _userAccountRepository;

    public UpdateReactiveSuspendUserHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public async Task<bool> Handle(UpdateReactiveSuspendUserCommand request, CancellationToken cancellationToken)
    {
        request.UserAccount.IsActive = true;
        _userAccountRepository.Update(request.UserAccount);
       return  await _userAccountRepository.SaveChanagesAsync();
    }
}