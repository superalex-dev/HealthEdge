namespace BackendProcessor.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string userEmail, string userName);

        Task SendWelcomeAdminEmail(string userEmail, string userName);
    }
}
