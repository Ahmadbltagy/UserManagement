using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class UpdateUserAccountLastLoginCommand : IRequest<bool>
{
    public UpdateUserAccountLastLoginCommand(UserAccount userAccount)
    {
        UserAccount = userAccount;
    }

    public UserAccount UserAccount { get; set; }
}