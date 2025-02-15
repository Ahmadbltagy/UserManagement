using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Interfaces.Services;

public interface ILoginService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}