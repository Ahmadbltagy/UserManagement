using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Queries;

public class GetUserProfileByUserIdQuery : IRequest<UserProfile?>
{
    public string UserId { get; }
    public GetUserProfileByUserIdQuery(string userId)
    {
        UserId = userId;
    }

    
    
}