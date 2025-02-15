namespace UsersManagement.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } 

}