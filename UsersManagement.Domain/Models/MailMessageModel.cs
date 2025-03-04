namespace UsersManagement.Domain.Models;

public class MailMessageModel
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string SmtpClient { get; set; }
    public string? CC { get; set; }
    public string? BCC { get; set; }
    public int Port { get; set; }
}