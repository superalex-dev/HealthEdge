﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendProcessor.Data;
using BackendProcessor.Repositories.Interfaces;
using BackendProcessor.Models;

namespace BackendProcessor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalDbContext _context;

        public UserRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task DeleteMultipleUsersAsync(IEnumerable<int> ids)
        {
            var users = _context.Users.Where(u => ids.Contains(u.Id));
            
            if (users.Any())
            {
                _context.Users.RemoveRange(users);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> EditUserAsync(int userId, User user)
        {
            if (userId != user.Id)
            {
                return null;
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }
        public async Task<User> GetUserByUsernameEmail(string username, string email)
        {
            return await _context.Users
                .Where(u => u.UserName == username || u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
