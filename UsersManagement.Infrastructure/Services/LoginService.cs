using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UsersManagement.Application.Commands;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Infrastructure.Services;

public class LoginService : ILoginService
{
    private IMediator _mediator;
    private IConfiguration _configuration;
    private ILogger<LoginService> _logger;
    public LoginService(
        IMediator mediator, 
        IConfiguration configuration, 
        ILogger<LoginService> logger)
    {
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
            var query = new GetUserAccountByEmailQuery(loginRequestDto.Email);
            var userAccountResult = await _mediator.Send(query);
          
            var isValidAccount = VerifyUserAccount(loginRequestDto.Password, userAccountResult);

            if (!isValidAccount)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            if (isValidAccount && userAccountResult.IsTempPassword) 
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Password change required."
                };
            }

            
            var (token,sessionId) = GenerateToken(userAccountResult);
        
            var isSessionSaved =  await SaveUserSession(userAccountResult, token, sessionId);
            
            if (!isSessionSaved)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Something went wrong."
                };
            }
            
            UpdateLastLoginDate(userAccountResult);
            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "login success",
                Token = token
            };
      
    }

    private async void UpdateLastLoginDate(UserAccount userAccount)
    {
       var query = new UpdateUserAccountLastLoginCommand(userAccount);
       var result = await _mediator.Send(query);
    }

    private bool  VerifyUserAccount(string password, UserAccount userAccount)
    {
        return BCrypt.Net.BCrypt.Verify(password, userAccount.IsTempPassword? userAccount.TempPassword : userAccount.Password);
    }

    private (string,Guid) GenerateToken(UserAccount userAccount)
    {
        var tt = _configuration["JWTConfig:SecretKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfig:SecretKey"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var sessionId = Guid.NewGuid();

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWTConfig:Issuer"],
            Audience = _configuration["JWTConfig:Audience"],
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, userAccount.UserId.ToString()),
                new(ClaimTypes.Name, userAccount.UserProfile.LastName),
                new(ClaimTypes.Role, userAccount.UserRole?.Name),
                new(ClaimTypes.Sid, sessionId.ToString())
                

            })
        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescription);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return (accessToken, sessionId);
        
       
    }

    private async Task<bool> SaveUserSession(UserAccount userAccount, string token, Guid sessionId)
    {
        var userSession = new UserSession()
        {
            UserAccountId = userAccount.Id,
            UserId = userAccount.UserId,
            Token = token,
            SessionId = sessionId,
            CreatedAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:ExpiresInMinutes"])), 
        };
        try
        {
            var command = new CreateUserSessionCommand(userSession);
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}