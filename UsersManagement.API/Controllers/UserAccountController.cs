using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Domain.Entities;

namespace UsersManagement.API.Controllers;


[ApiController]
[Route("api/userAccount")]

public class UserAccountController : ControllerBase
{
    
    private IUserAccountService _userAccountService;
    const int MAX_PAGE_SIZE = 100;
    public UserAccountController(IUserAccountService userAccountService)
    {
        _userAccountService = userAccountService;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> NewUserAccount(CreateUserAccountDto createUserAccountDto)
    {
        var createUserAccount = await _userAccountService.CreateAccount(createUserAccountDto);

        if (!createUserAccount.IsSuccess)
        {
            if (createUserAccount.Message == "Email already exists.")
            {
                return Conflict(new { message = createUserAccount.Message });
            }
            return StatusCode(500, new {
                message= "An unexpected error occurred while creating the account. Please try again later."
            });
        }
        
            return Ok(new {message = createUserAccount.Message});
    }

    [HttpGet("AllActive")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetAllActiveUserAccounts(int pageNumber, int pageSize)
    {
        if(pageNumber < 1) pageNumber = 1;
        if(pageSize > MAX_PAGE_SIZE) pageSize = MAX_PAGE_SIZE;
        
        return Ok(await _userAccountService.GetUserAccounts(pageNumber, pageSize,true));
    }
    
    
    [HttpGet("AllInactive")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllInActiveUserAccounts(int pageNumber, int pageSize)
    {
        if(pageNumber < 1) pageNumber = 1;
        if(pageSize > MAX_PAGE_SIZE) pageSize = MAX_PAGE_SIZE;
        return Ok(await _userAccountService.GetUserAccounts(pageNumber, pageSize, false));
    }
    
    [HttpPut("Reactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReactiveUserByEmail(string email)
    {
        var reactive = await _userAccountService.ReactiveUserAccounts(email);
        if (reactive.IsSuccess)
        {
            return Ok(reactive);
        }
        
        return reactive.Message switch
        {
            "User not found." => NotFound(reactive),
            "Account is active."=> BadRequest(reactive),
            _ => StatusCode(500, reactive)
        };
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> UpdateUserAccountPassword(UpdateUserAccountPasswordDto createUserAccountDto)
    {
        var updatePassword = await _userAccountService.UpdatePassword(createUserAccountDto);

        if (!updatePassword.IsSuccess)
        {
            return updatePassword.Message switch
            {
                "Invalid email or password." => Unauthorized(updatePassword),
                "Cannot update password." => StatusCode(StatusCodes.Status500InternalServerError, updatePassword),
                _ => BadRequest(updatePassword) 
            };
        }
        
        return Ok(updatePassword);
    }
}