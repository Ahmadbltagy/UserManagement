using MediatR;
using UsersManagement.Application.Common;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Application.Commands.Handlers;

public class CreateUserAccountHandler : IRequestHandler<CreateUserAccountCommand, bool>
{
    private IUserAccountRepository _userAccountRepository;

    public CreateUserAccountHandler(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public async Task<bool> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var userAccount = new UserAccount
        {
            Email = request.CreateUserAccountDto.Email.ToLower(),
            UserId = userId,
            TempPassword =  PasswordHasher.HashPassword(request.CreateUserAccountDto.TempPassword),
            IsTempPassword = true,
            CreatedAt = DateTime.Now,
            IsActive = true,
            UserRoleId = (int) request.CreateUserAccountDto.UserRoleId,
            UserProfile = new UserProfile
            {
                FirstName = request.CreateUserAccountDto.FirstName,
                LastName = request.CreateUserAccountDto.LastName,
                DateOfBirth = request.CreateUserAccountDto.DateOfBirth,
                UserId = userId
            }
        };
        await _userAccountRepository.AddAsync(userAccount);
        return await _userAccountRepository.SaveChanagesAsync();
        
    }
}