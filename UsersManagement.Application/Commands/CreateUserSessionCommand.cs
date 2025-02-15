using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class CreateUserSessionCommand : IRequest<bool>
{
    public UserSession UserSession { get; set; }
    
    public CreateUserSessionCommand(UserSession userSession)
    {
        UserSession = userSession;
    }
}