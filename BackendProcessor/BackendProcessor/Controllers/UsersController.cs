﻿using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using BackendProcessor.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpGet("users/count")]
        public async Task<IActionResult> GetTotalUsersCountAsync()
        {
            return Ok(await _userRepository.GetTotalUsersCountAsync());
        }

        [HttpGet("users/get")]
        public async Task<IActionResult> GetUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetUsersAsync();

            if (users == null || users.Count() ==0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("users/get/{Id}")]
        public async Task<IActionResult> GetUserByIdAsync(int Id)
        {
            var user = await _userRepository.GetUserByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationDto userDto)
        {
            var existingUser = await _userRepository.GetUserByUsernameEmail(userDto.UserName, userDto.Email);
            var now = DateTime.UtcNow;

            if (existingUser != null)
            {
                return Conflict("A user with this username or email already exists.");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                DateOfCreation = now,
                ContactNumber = userDto.ContactNumber,
                DateOfBirth = userDto.DateOfBirth
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            var userReturnDto = new UserDto(
                createdUser.Id,
                createdUser.FirstName,
                createdUser.LastName,
                createdUser.UserName,
                createdUser.Email,
                createdUser.DateOfCreation,
                createdUser.ContactNumber,
                createdUser.DateOfBirth);

            await _emailService.SendWelcomeAdminEmail(user.Email, user.UserName);

            return Ok(userReturnDto);
        }

        [HttpPut("users/edit/{Id}")]
        public async Task<IActionResult> EditUserAsync(int Id, [FromBody] EditUserDto userDto)
        {
            var currentUser = await _userRepository.GetUserByIdAsync(Id);

            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            if (!string.IsNullOrEmpty(userDto.Email) && currentUser.Email != userDto.Email)
            {
                var existingUserWithEmailOrUserName = await _userRepository.GetUserByUsernameEmail(userDto.Email, userDto.UserName);
                if (existingUserWithEmailOrUserName != null)
                {
                    return Conflict("A user with this email or user name already exists.");
                }
            }

            var editUserDto = new EditUserDto(currentUser.Id, currentUser.FirstName, currentUser.LastName, currentUser.UserName, currentUser.Email, currentUser.ContactNumber, currentUser.DateOfBirth);

            editUserDto.FirstName = userDto.FirstName;
            editUserDto.LastName = userDto.LastName;
            editUserDto.Email = userDto.Email;
            editUserDto.UserName = userDto.UserName;
            editUserDto.ContactNumber = userDto.ContactNumber;
            editUserDto.DateOfBirth = userDto.DateOfBirth;

            var editedUser = await _userRepository.EditUserAsync(Id, editUserDto);

            if (editedUser == null)
            {
                return BadRequest();
            }

            return Ok(editedUser);
        }

        [HttpDelete("users/delete/{Id}")]
        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            await _userRepository.DeleteUserAsync(Id);
            
            return NoContent();
        }
        
        [HttpDelete("users/delete-multiple")]
        public async Task<IActionResult> DeleteMultipleUsersAsync([FromBody] IEnumerable<int> userIds)
        {
            await _userRepository.DeleteMultipleUsersAsync(userIds);

            return Ok();
        }
    }
}
