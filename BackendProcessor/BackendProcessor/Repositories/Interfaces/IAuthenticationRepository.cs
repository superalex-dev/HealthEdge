using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticatedResponseModel> Login(UserLogin user);

        Task<bool> AdminLogin(UserLogin user);
    }
}
