using MediatR;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class UpdateUserAccountCommand : IRequest<bool>
{
    public UpdateUserAccountPasswordDto UpdateUserAccountPasswordDto { get;}
    public UserAccount UserAccount { get; set; }
    public UpdateUserAccountCommand(
        UpdateUserAccountPasswordDto updateUserAccountPasswordDto, 
        UserAccount userAccount)
    {
        UpdateUserAccountPasswordDto = updateUserAccountPasswordDto;
        UserAccount = userAccount;
    }

}