using Activictiy.Application.Abstraction.Services;
using Activictiy.Domain.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace Activictiy.Persistance.Implementations.Service;

public class EmailService : IEmailService
{
    private readonly EmailSetting _emailSetting;
    private readonly IConfiguration _configuration;

    public EmailService(EmailSetting emailSetting, IConfiguration configuration)
    {
        _emailSetting = emailSetting;
        _configuration = configuration;
    }

    public void sendEmail(string to, string subject, string html, string from = null)
    {
        // create email message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _emailSetting.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        var smtp = new SmtpClient();
        smtp.Connect(_emailSetting.SmtpServer, _emailSetting.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSetting.Username, _emailSetting.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"http://localhost:3000/ResertPassword/");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>LD - LuxeDrive");

        sendEmail(to, "Şifre Yenileme Talebi", mail.ToString());
    }
}