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
            var plainTextContent = $"Dear {userName},\n\nThank you for registering! Please read our terms of service here: https://docs.google.com/document/d/1FrodI2JhR99iOWQzPlniHNKRHVYD20zALzqaopPsxxk/edit?usp=sharing";

            var htmlContent = $@"
                <html>
                    <body>
                        <h1>Welcome to HealthEdge, {userName}!</h1>
                        <p>Thank you for registering. We're excited to have you on board.</p>
                        <p>Please read our <a href='https://docs.google.com/document/d/1FrodI2JhR99iOWQzPlniHNKRHVYD20zALzqaopPsxxk/edit?usp=sharing'>terms of service</a>.</p>
                        <p>Best regards,</p>
                        <p>Alexander Boev - HealthEdge CEO</p>
                    </body>
                </html>";

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
