namespace UsersManagement.Application.DTOs;

public class UserSessionInfoDto
{
    public string Email { get; set; }
    public string SessionId { get; set; }
    public string UserId { get; set; }
    public string DeviceInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
}