namespace UsersManagement.Domain.Entities;

public class UserSession
{
    public int Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string? DeviceInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } 
}

