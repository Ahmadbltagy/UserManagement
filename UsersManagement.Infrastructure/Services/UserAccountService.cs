using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UsersManagement.Application.Commands;
using UsersManagement.Application.Common;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Models;

namespace UsersManagement.Infrastructure.Services;

public class UserAccountService : IUserAccountService
{
    private IMediator _mediator;
    private ILogger<UserAccountService> _logger;
    private IConfiguration _configuration;
    private IEmailService _emailService;

    public UserAccountService(
        IMediator mediator, 
        ILogger<UserAccountService> logger, 
        IConfiguration configuration, 
        IEmailService emailService)
    {
        _mediator = mediator;
        _logger = logger;
        _configuration = configuration;
        _emailService = emailService;
    }


    public async Task<UserAccountResponseDto> CreateAccount(CreateUserAccountDto createUserAccountDto)
    {
        
        var query = new IsEmailExistsQuery(createUserAccountDto.Email);
        var isEmailExists = await _mediator.Send(query);
        if (isEmailExists)
        {
            return new UserAccountResponseDto
            {
                IsSuccess = false,
                Message = "Email already exists."
            };
        }
        
        var command = new CreateUserAccountCommand(createUserAccountDto);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return new UserAccountResponseDto
            {
                IsSuccess = false,
                Message = "Cannot create a new account."
            };
            
        }
        _logger.LogInformation($"User {createUserAccountDto.Email} created");

        try
        {
            SendMail(createUserAccountDto.FirstName, createUserAccountDto.Email, createUserAccountDto.TempPassword);
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR: {ex.Message}");
        }
        return new UserAccountResponseDto
        {
            IsSuccess = true,
            Message = $"User {createUserAccountDto.Email} created"
        };

    }

    public async Task<UserAccountResponseDto> UpdatePassword(UpdateUserAccountPasswordDto updateUserAccountPasswordDto)
    {
        
        var query = new GetUserAccountByEmailQuery(updateUserAccountPasswordDto.Email);
        var userAccount = await _mediator.Send(query);

        if (updateUserAccountPasswordDto.OldPassword == updateUserAccountPasswordDto.NewPassword)
        {
            return new UserAccountResponseDto()
            {
                IsSuccess = false,
                Message = "New password must be different from the old password."
            };
        }
        if (userAccount == null)
        {
            return new UserAccountResponseDto
            {
                IsSuccess = false,
                Message = "Invalid email or password."
            };
        }
        
        var isValidPassword = PasswordHasher.VerifyPassword(updateUserAccountPasswordDto.OldPassword, userAccount.IsTempPassword? userAccount.TempPassword: userAccount.Password);
        
        if (!isValidPassword)
        {
            return new UserAccountResponseDto
            {
                IsSuccess = false,
                Message = "Invalid email or password."
            };    
        }
        
        var command = new UpdateUserAccountCommand(updateUserAccountPasswordDto, userAccount);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return new UserAccountResponseDto
            {
                IsSuccess = false,
                Message = "Cannot update password."
            };
        }
        
        return new UserAccountResponseDto
        {
            IsSuccess = true,
            Message = "Password updated."
        };
    }

    public async Task<UserAccountResponseDto> ReactiveUserAccounts(string email)
    {
       var userAccount = await GetUserAccountByEmail(email);
       if (userAccount == null)
       {
           return new UserAccountResponseDto
           {
               IsSuccess = false,
               Message = "User not found."
           };
       }

       if (userAccount.IsActive)
       {
           return new UserAccountResponseDto
           {
               IsSuccess = false,
               Message = "Account is active."
           };
       }
       
       var command = new UpdateReactiveSuspendUserCommand(userAccount);
       var result = await _mediator.Send(command);
       if (!result)
       {
           return new UserAccountResponseDto
           {
               IsSuccess = false,
               Message = "Cannot update this user."
           };
       }

       return new UserAccountResponseDto
       {
           IsSuccess = true,
           Message = $"User {userAccount.Email} reactive."
       };
    }

    private async Task<UserAccount?> GetUserAccountByEmail(string email)
    {
        var userAccountQuery = new GetUserAccountByEmailQuery(email);
        return await _mediator.Send(userAccountQuery);
    }

    public async Task<PaginatedResponse<UserAccountWithProfileDto>> GetUserAccounts(int pageNumber,int pageSize, bool isActive)
    {
        var query = new GetUserAccountQuery(pageNumber, pageSize, isActive);
        return await _mediator.Send(query);
    }

    private void SendMail(string firstName,string email, string tempPassword)
    {
        var mailBody = _configuration["MailConfig:Body"]
            .Replace("%UserName%", firstName)
            .Replace("%Email%", email)
            .Replace("%TempPassword%", tempPassword);
        
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
            Subject = _configuration["MailConfig:MailSubject"]!,
            Body = mailBody
        };
        
        _emailService.SendEmail(mailMessage);
    }
}