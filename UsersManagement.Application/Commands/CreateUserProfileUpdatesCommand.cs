using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class CreateUserProfileUpdatesCommand : IRequest<bool>
{
    public UserProfilePendingUpdates UserProfilePendingUpdates { get; }

    public CreateUserProfileUpdatesCommand(UserProfilePendingUpdates userProfilePendingUpdates)
    {
        UserProfilePendingUpdates = userProfilePendingUpdates;
    }
    
}