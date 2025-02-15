using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Interfaces.Services;

namespace UsersManagement.API.Controllers;

[ApiController]
[Route("api/userProfileUpdates")]
[Authorize(Roles = "Admin")]
public class UserProfileUpdatesController : ControllerBase
{

    const int MAX_PAGE_SIZE = 100;
    private readonly IUserProfileUpdatesService _userProfileUpdatesService;

    public UserProfileUpdatesController(IUserProfileUpdatesService userProfileUpdatesService)
    {
        _userProfileUpdatesService = userProfileUpdatesService;
    }


    [HttpGet("AllPendingRequests")]
    public async Task<IActionResult> GetUserProfileUpdates(int pageNumber, int pageSize)
    {
        if(pageSize > MAX_PAGE_SIZE) pageSize = MAX_PAGE_SIZE;
        if(pageNumber < 1) pageNumber = 1;
        
        var pendingRequests = await _userProfileUpdatesService.GetUserProfileUpdates(pageNumber, pageSize);
        
        return Ok(pendingRequests);
    }

    [HttpPut("ApprovePendingRequests")]
    
    public async Task<IActionResult> ApprovePendingRequests(int requestId)
    {
        var result = await _userProfileUpdatesService.ApproveUserRequest(requestId);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        
        return result.Message switch
        {
            "Request not found." => NotFound(result),
            "User profile not updated."=> StatusCode(StatusCodes.Status500InternalServerError, result),
            _ => BadRequest(result)
        };
    
    }
}