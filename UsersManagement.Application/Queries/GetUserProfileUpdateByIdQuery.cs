using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserProfileUpdateByIdQuery : IRequest<UserProfileUpdates?>
{
    public int RequestId { get;  }

    public GetUserProfileUpdateByIdQuery(int requestId)
    {
        RequestId = requestId;
    }
}