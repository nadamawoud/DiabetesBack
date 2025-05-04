using System.Net;
using System.Net.Mail;
using Diabetes.Core.Interfaces;
using Microsoft.Extensions.Configuration;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendVerificationEmailAsync(string email, string verificationCode)
    {
        try
        {
            var emailConfig = _configuration.GetSection("EmailConfiguration");
            var from = emailConfig["From"];
            var smtpServer = emailConfig["SmtpServer"];
            var port = int.Parse(emailConfig["Port"]);
            var userName = emailConfig["UserName"];
            var password = emailConfig["Password"];

            var fromAddress = new MailAddress(from, "Diabetes Management System");
            var toAddress = new MailAddress(email);
            
            using (var message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = "Email Verification Code";
                message.Body = $"Your verification code is: {verificationCode}";
                message.IsBodyHtml = false;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = smtpServer;
                    smtp.Port = port;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(userName, password);

                    await smtp.SendMailAsync(message);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}");
        }
    }
}