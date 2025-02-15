using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.Interfaces.Repositories;

namespace UsersManagement.Application.Commands.Handlers;

public class UpdateUserAccountHandler : IRequestHandler<UpdateUserAccountCommand, bool>
{
    private IUserAccountRepository _userAccountRepository;

    public UpdateUserAccountHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    
    public async Task<bool> Handle(UpdateUserAccountCommand request, CancellationToken cancellationToken)
    {
        request.UserAccount.Password = PasswordHasher.HashPassword(request.UpdateUserAccountPasswordDto.NewPassword);
        request.UserAccount.IsTempPassword = false;
        
        _userAccountRepository.Update(request.UserAccount);
       return await _userAccountRepository.SaveChanagesAsync();
    }
}