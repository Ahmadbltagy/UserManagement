using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class ApproveUserProfileUpdateRequestCommand : IRequest<bool>
{
    public ApproveUserProfileUpdateRequestCommand(UserProfilePendingUpdates userProfilePendingUpdates)
    {
        UserProfilePendingUpdates = userProfilePendingUpdates;
    }

    public UserProfilePendingUpdates UserProfilePendingUpdates { get; set; }    
    
}