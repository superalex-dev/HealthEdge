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

        public async Task SendWelcomeEmail(string userEmail, string userName)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("mr.naidobrixjr@gmail.com", "Alexander Boev - HealthEdge CEO");
            var to = new EmailAddress(userEmail, userName);
            var subject = "Welcome to HealthEdge";
            var message = $"Dear {userName},\n\nThank you for registering!";
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

        public async Task SendWelcomeAdminEmail(string userEmail, string userName)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("mr.naidobrixjr@gmail.com", "Alexander Boev - HealthEdge CEO");
            var to = new EmailAddress(userEmail, userName);
            var subject = "Welcome to our admin service";
            var message = $"Dear {userName},\n\nYou have been granted admin access!";
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
