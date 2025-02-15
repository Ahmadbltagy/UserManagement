using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Infrastructure.Services;

namespace UsersManagement.API.Controllers;

[ApiController]
[Route("api/login")]

public class LoginController : ControllerBase
{
    private ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        var result = await _loginService.Login(loginRequestDto);
        
        if (!result.IsSuccess)
        {
            return result.Message switch
            {
                "Invalid email or password" => NotFound(new {message = result.Message}),
                _=> StatusCode(StatusCodes.Status403Forbidden, new {message = result.Message})
            };
        }
        
        return Ok(new {message = result.Message, token = result.Token});
    }
}