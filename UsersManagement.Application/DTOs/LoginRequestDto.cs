using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Application.DTOs;

public class LoginRequestDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}