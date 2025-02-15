using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Application.DTOs;

public class UpdateUserAccountPasswordDto
{
    [EmailAddress]
    public string Email { get; set; }
    
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
