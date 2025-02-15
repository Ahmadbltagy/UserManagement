namespace UsersManagement.Domain.Entities;

public class UserProfileUpdates
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime InsertionDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public bool IsApproved { get; set; }
    
    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } 

}
