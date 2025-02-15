using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class SuspendInActiveUserAccountCommand : IRequest<bool>
{
    public List<UserAccount> UserAccounts { get; set; }

    public SuspendInActiveUserAccountCommand(List<UserAccount> userAccounts)
    {
        UserAccounts = userAccounts;
    }
}