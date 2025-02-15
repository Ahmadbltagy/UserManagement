using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class UpdateReactiveSuspendUserCommand : IRequest<bool>
{
    public UpdateReactiveSuspendUserCommand(UserAccount userAccount)
    {
        UserAccount = userAccount;
    }

    public UserAccount UserAccount { get; set; }
}