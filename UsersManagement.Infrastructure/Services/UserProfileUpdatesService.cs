using MediatR;
using Microsoft.Extensions.Configuration;
using UsersManagement.Application.Commands;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Models;

namespace UsersManagement.Infrastructure.Services;

public class UserProfileUpdatesService : IUserProfileUpdatesService
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    
    public UserProfileUpdatesService(IMediator mediator, IConfiguration configuration, IEmailService emailService)
    {
        _mediator = mediator;
        _configuration = configuration;
        _emailService = emailService;
    }
    
    public async Task<PaginatedResponse<UserProfilePendingUpdatesResponseDto>> GetUserProfileUpdates(int pageNumber, int pageSize)
    {
        var query = new GetAllPendingUserProfileUpdatesQuery(pageNumber, pageSize);
        
        var result = await _mediator.Send(query);
        
        return result;
    }

    public async Task<UserApproveResponseDto> ApproveUserRequest(int requestId)
    {
        var userProfileUpdateQuery = new GetUserProfileUpdateByIdQuery(requestId);
        var userProfileUpdatesResult = await _mediator.Send(userProfileUpdateQuery);

        if (userProfileUpdatesResult == null)
        {
            return new UserApproveResponseDto()
            {
                 IsSuccess   = false,
                 Message = "Request not found."
            };
        }

        if (userProfileUpdatesResult.IsApproved)
        {
            return new UserApproveResponseDto()
            {
                IsSuccess   = false,
                Message = "Request already approved."
            };
        }
            
        var approveUserRequest = new ApproveUserProfileUpdateRequestCommand(userProfileUpdatesResult);
        var approveUserResult = await _mediator.Send(approveUserRequest);

        if (!approveUserResult)
        {
            return new UserApproveResponseDto()
            {
                IsSuccess   = false,
                Message = "User profile not updated."
            };
        }
        
        var isUpdated =  await UpdateUserProfileTable(userProfileUpdatesResult);

        if (!isUpdated)
        {
            return new UserApproveResponseDto
            {
                IsSuccess = false,
                Message = "User profile not updated."
            };
        }

       await SendApprovalMail(userProfileUpdatesResult.UserAccountId);
           
        return new UserApproveResponseDto()
        {
            IsSuccess = true,
            Message = "User profile updated."
        };
    }

    private async Task SendApprovalMail(int userAccountId)
    {
        var email = await GetUserEmail(userAccountId);
        
        var mailMessage = new MailMessageModel()
        {
            Username = _configuration["MailConfig:MailUsername"]!,
            Password = _configuration["MailConfig:MailPassword"]!,
            SmtpClient = _configuration["MailConfig:SmtpServer"]!,
            Port = Convert.ToInt32(_configuration["MailConfig:Port"]),
            From = _configuration["MailConfig:MailAddress"]!,
            To = email,
            CC = _configuration["MailConfig:CC"]!,
            BCC = _configuration["MailConfig:BCC"]!,
            Subject = "Account Update Approval.",
            Body = "Your updates for the account have been approved."
        };
        _emailService.SendEmail(mailMessage);

    }

    private async Task<string> GetUserEmail(int userAccountId)
    {
        var query = new GetUserAccountByIdQuery(userAccountId);
        var result = await _mediator.Send(query);
        return result.Email;
    }

    private async Task<bool> UpdateUserProfileTable(UserProfilePendingUpdates userProfilePendingUpdatesResult)
    {
           
        var userProfileQuery = new GetUserProfileByUserIdQuery(userProfilePendingUpdatesResult.UserId.ToString());
        var userProfileResult = await _mediator.Send(userProfileQuery);

        var updateUserProfileDto = new UpdateUserProfileDto
        {
            FirstName = userProfilePendingUpdatesResult.FirstName,
            LastName = userProfilePendingUpdatesResult.LastName,
            DateOfBirth = userProfilePendingUpdatesResult.DateOfBirth,
        };

        var updateUserProfile = new UpdateUserProfileCommand(updateUserProfileDto, userProfileResult);
        return await _mediator.Send(updateUserProfile);
    }
}