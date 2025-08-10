namespace Activictiy.Application.Abstraction.Services;

public interface IEmailService
{
    void sendEmail(string to, string subject, string html, string from = null);
    Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
}
