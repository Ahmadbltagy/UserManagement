using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Domain.Entities;

public class UserAccount
{
    public int Id { get; set; }
    public Guid UserId { get; set; } = new Guid();
    [EmailAddress]
    public string Email { get; set; }
    public string? TempPassword { get; set; }
    public string? Password { get; set; }
    public bool IsTempPassword { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    
    public int? UserRoleId { get; set; }
    public UserRole UserRole { get; set; }
    public UserProfile UserProfile { get; set; } 
    public ICollection<UserSession> UserSession { get; set; }
    public ICollection<UserProfileUpdates> UserProfileUpdates { get; set;}
}