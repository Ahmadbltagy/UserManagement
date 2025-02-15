namespace UsersManagement.Domain.Entities;

public class UserRole
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<UserAccount> UserAccounts { get; set; } 
}