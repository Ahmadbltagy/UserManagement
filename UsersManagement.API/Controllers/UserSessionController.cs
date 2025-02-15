using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries;
using UsersManagement.Domain.Entities;

namespace UsersManagement.API.Controllers;

[ApiController]
[Route("api/userSession")]
[Authorize(Roles = "SuperAdmin")]
public class UserSessionController : ControllerBase
{
    private readonly IUserSessionService _userSessionService;

    public UserSessionController(IUserSessionService userSessionService)
    {
        _userSessionService = userSessionService;
    }

    [HttpGet("AllEmployeeSession")]
    public async Task<IActionResult> GetAllEmployeeSession()
    {
        return Ok(await _userSessionService.GetAllEmployeeSessions());
    }

    [HttpDelete("EmployeeSession")]
    public async Task<IActionResult> DeleteEmployeeSession(string sessionId)
    {
        var delete = await _userSessionService.DeleteUserSession(sessionId);

        if (delete.IsSuccess)
        {
            return Ok(delete);
        }

        return delete.Message switch
        {
            "UserSession not found." => NotFound(delete),
            _ => StatusCode(500, delete)
        };
    }

   
}