using MediatR;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Commands;

public class UpdateUserProfileCommand : IRequest<bool>
{
    public UpdateUserProfileDto UpdateUserProfileDto { get; }
    public UserProfile UserProfile { get; }
    
    public UpdateUserProfileCommand(UpdateUserProfileDto updateUserProfileDto, UserProfile userProfile)
    {
        UpdateUserProfileDto = updateUserProfileDto;
        UserProfile = userProfile;
    }

}