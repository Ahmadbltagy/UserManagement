using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Domain.Models;

namespace UsersManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    private ILogger<EmailService> _logger;
    
    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }
    
    public void SendEmail(MailMessageModel mail)
    {
        var m = new System.Net.Mail.MailMessage();
        m.From = new MailAddress(mail.From);
        m.Subject = mail.Subject;
        m.Body = mail.Body;

        m.IsBodyHtml = true;

        if (!string.IsNullOrEmpty(mail.To))
        {
            string[] ar = mail.To.Split(',');
            foreach (string address in ar)
                m.To.Add(address);
        }

        if (!string.IsNullOrEmpty(mail.CC))
        {
            string[] ar = mail.CC.Split(',');
            foreach (string address in ar)
                m.CC.Add(address);
        }

        if (!string.IsNullOrEmpty(mail.BCC))
        {
            string[] ar = mail.BCC.Split(',');
            foreach (string address in ar)
                m.Bcc.Add(address);
        }


        using var emailClient = new SmtpClient(mail.SmtpClient)
        {
            Credentials = new NetworkCredential(mail.Username, mail.Password),
            EnableSsl = true,
            Port = mail.Port == 0 ? 587 : mail.Port,
        };
        emailClient.Send(m);

    }
}