using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class ApproveUserProfileUpdateRequestCommand : IRequest<bool>
{
    public ApproveUserProfileUpdateRequestCommand(UserProfileUpdates userProfileUpdates)
    {
        UserProfileUpdates = userProfileUpdates;
    }

    public UserProfileUpdates UserProfileUpdates { get; set; }    
    
}