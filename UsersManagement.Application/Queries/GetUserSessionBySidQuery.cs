using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserSessionBySidQuery : IRequest<UserSession?>
{
    public GetUserSessionBySidQuery(string sessionId)
    {
        SessionId = sessionId;
    }

    public string SessionId { get; set; }
}