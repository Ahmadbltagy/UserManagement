using System.ComponentModel.DataAnnotations;
using UsersManagement.Application.Validators;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Application.DTOs;

public class CreateUserAccountDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string TempPassword { get; set; }
    [ValidEnum(typeof(UserRoleEnum))]
    public UserRoleEnum UserRoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}