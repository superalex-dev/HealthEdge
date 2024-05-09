using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;

namespace BackendProcessor.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ApiKey"];
        }

        public async Task SendWelcomeEmail(string userEmail, string userName, string fromEmail, string fromName, string subject, string message)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(userEmail, userName);
            var plainTextContent = message;
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email", ex);
            }
        }
    }
}
