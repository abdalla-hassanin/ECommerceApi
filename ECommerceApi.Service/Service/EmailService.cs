using System.Net;
using System.Net.Mail;
using ECommerceApi.Data.Options;
using ECommerceApi.Service.IService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECommerceApi.Service.Service;

public class EmailService : IEmailService
{
    private readonly SecretOptions _secrets;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<SecretOptions> secrets, ILogger<EmailService> logger)
    {
        _secrets = secrets.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation("Attempting to send email to {ToEmail} with subject: {Subject}", toEmail, subject);

        var smtpSettings = _secrets.EmailSettings;

        var smtpClient = new SmtpClient(smtpSettings.Host)
        {
            Port = smtpSettings.Port,
            Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings.UserName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while sending email to {ToEmail}. Subject: {Subject}", toEmail,
                subject);
            throw new Exception($"Exception occurred while sending email to {toEmail}. Subject: {subject}", ex);
        }
    }
}