using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;

namespace UsersManagement.API.Controllers;


[ApiController]
[Route("api/userProfile")]
[Authorize]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    
    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }


    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateUserProfileDto userProfile)
    {
       var userProfileResponse =  await _userProfileService.UpdateUserProfile(userProfile);

       if (userProfileResponse.IsSuccess)
       {
         return Ok(userProfileResponse);
       }
       
       return userProfileResponse.Message switch
       {
           "Invalid credentials." => Unauthorized(new { message = userProfileResponse.Message }),
           "User profile not found." => NotFound(new { message = userProfileResponse.Message }),
           _ => StatusCode(StatusCodes.Status500InternalServerError, new { message = userProfileResponse.Message })
       };
    }
    
    [HttpPut("UpdateProfileByUserId")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateProfileByUserId(UpdateUserProfileByIdDto updateUserProfile)
    {
        var userProfileResponse =  await _userProfileService.UpdateUserProfileById(updateUserProfile);
        if(userProfileResponse.IsSuccess) return Ok(userProfileResponse);
        
        return userProfileResponse.Message switch
        {
            "User profile not found." => NotFound(userProfileResponse),
            _ => BadRequest(userProfileResponse)
        };
    }
}