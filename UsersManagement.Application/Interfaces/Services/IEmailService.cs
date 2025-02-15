using UsersManagement.Domain.Models;

namespace UsersManagement.Application.Interfaces.Services;

public interface IEmailService
{
    void SendEmail(MailMessageModel mail);
}