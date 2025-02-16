using MediatR;
using Microsoft.Extensions.Logging;
using UsersManagement.Application.Commands;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Infrastructure.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;
    private readonly ILogger<UserProfileService> _logger;

    public UserProfileService(
        IMediator mediator, 
        IJwtService jwtService, 
        ILogger<UserProfileService> logger)
    {
        _mediator = mediator;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<UserProfileResponseDto> UpdateUserProfile(UpdateUserProfileDto userProfileDto)
    {
        var userId =  _jwtService.GetUserId();
        var userRole = _jwtService.GetUserRole();

        if (userId == null || userRole == null)
        {
            return new UserProfileResponseDto
            {
                IsSuccess = false,
                Message = "Invalid credentials."
            };
        }

        var userProfileResult = await GetUserProfile(userId);
        
        if (userProfileResult == null)
        {
            return new UserProfileResponseDto
            {
                IsSuccess = false,
                Message = "User profile not found."
            };
        }
        
        _logger.LogInformation($"User {userId} is proceeding to update their profile.");

        switch (userRole)
        {
            case "SuperAdmin":
            case "Admin":
                return await UpdateAdminUserProfile(userProfileDto, userProfileResult);
            case "Employee":
                return await CreatePendingEmployeeProfileUpdate(userProfileDto, userProfileResult);
            default:
                return new UserProfileResponseDto()
                {
                    IsSuccess = false,
                    Message = "Invalid user role."
                };
        }
        
    }

 

    public async Task<UserProfileResponseDto> UpdateUserProfileById(UpdateUserProfileByIdDto userProfileDto)
    {
        var userProfileResult = await GetUserProfile(userProfileDto.UserId);
        
        if (userProfileResult == null)
        {
            return new UserProfileResponseDto
            {
                IsSuccess = false,
                Message = "User profile not found."
            };
        }
        
        var userRole = await GetUserRole(userProfileResult.UserAccountId);
        
        if(userRole ==(int)UserRoleEnum.Admin)
            return await UpdateAdminUserProfile(userProfileDto, userProfileResult);
        
        return new UserProfileResponseDto
        {
            IsSuccess = false,
            Message = "Cannot update this profile."
        };
    }

    private async Task<int> GetUserRole(int userAccountId)
    {
        var userAccount = new GetUserAccountByIdQuery(userAccountId);
        var result =  await _mediator.Send(userAccount);
        return result?.UserRoleId ?? 0;
    }
    private async Task<UserProfile?> GetUserProfile(string userId)
    {
        var userProfileQuery = new GetUserProfileByUserIdQuery(userId);
        return await _mediator.Send(userProfileQuery);
    }
    
    private async Task<UserProfileResponseDto> UpdateAdminUserProfile(UpdateUserProfileDto userProfileDto, UserProfile userProfile)
    {
        var updateCommand = new UpdateUserProfileCommand(userProfileDto, userProfile);
        var updateResult = await _mediator.Send(updateCommand);
            
        if (!updateResult)
        {
            return new UserProfileResponseDto()
            {
                IsSuccess = false,
                Message = "Something went wrong."
            };
        }

        return new UserProfileResponseDto()
        {
            IsSuccess = true,
            Message = "Your profile has been updated."
        };
    }

    private async Task<UserProfileResponseDto> CreatePendingEmployeeProfileUpdate(UpdateUserProfileDto userProfileDto, UserProfile userProfile)
    {
        var userProfileUpdates = new UserProfilePendingUpdates()
        {
            FirstName = userProfileDto.FirstName ?? userProfile.FirstName,
            LastName = userProfileDto.LastName ?? userProfile.LastName,
            DateOfBirth = userProfileDto.DateOfBirth ?? userProfile.DateOfBirth,
            IsApproved = false,
            InsertionDate = DateTime.Now,
            ApprovalDate = null,
            UserId = userProfile.UserId,
            UserAccountId = userProfile.UserAccountId
        };
            
        var command = new CreateUserProfileUpdatesCommand(userProfileUpdates);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return new UserProfileResponseDto()
            {
                IsSuccess = false,
                Message = "Something went wrong."
            };   
        }
            
        return new UserProfileResponseDto()
        {
            IsSuccess = true,
            Message = "Update is pending approval from the admin."
        };
    }
    
}