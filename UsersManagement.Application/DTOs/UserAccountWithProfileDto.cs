namespace UsersManagement.Application.DTOs;

public class UserAccountWithProfileDto
{
    public string Email { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string UserRole { get; set; }
}