namespace UsersManagement.Application.DTOs;

public class UserProfilePendingUpdatesResponseDto
{
    public int RequestId { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime InsertionDate { get; set; }
}