using UsersManagement.Application.DTOs;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Interfaces.Services;

public interface IUserProfileService
{
    Task<UserProfileResponseDto> UpdateUserProfile(UpdateUserProfileDto userProfile);
    Task<UserProfileResponseDto> UpdateUserProfileById(UpdateUserProfileByIdDto userProfileDto);

}