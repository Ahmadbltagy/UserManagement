using MediatR;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class CreateUserProfileUpdatesCommand : IRequest<bool>
{
    public UserProfileUpdates UserProfileUpdates { get; }

    public CreateUserProfileUpdatesCommand(UserProfileUpdates userProfileUpdates)
    {
        UserProfileUpdates = userProfileUpdates;
    }
    
}