using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class DeleteUserSessionCommand : IRequest<bool>
{
    public UserSession UserSession { get;  }

    public DeleteUserSessionCommand(UserSession userSession)
    {
        UserSession = userSession;
    }
}