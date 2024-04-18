using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> EditUserAsync(int Id, EditUserDto userDto);
        Task DeleteUserAsync(int Id);
        Task DeleteMultipleUsersAsync(IEnumerable<int> ids);
        Task<User> GetUserByIdAsync(int Id);
        Task<int> GetTotalUsersCountAsync();
        Task<User> GetUserByUsernameEmail(string username, string email);
    }
}
