namespace BackendProcessor.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string userEmail, string userName, string fromEmail, string fromName, string subject, string message);
    }
}
