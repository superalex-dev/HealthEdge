using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userDto.Password,
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)),
                DateOfCreation = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            var userReturnDto = new UserDto
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                UserName = createdUser.UserName,
                Email = createdUser.Email,
                DateOfCreation = createdUser.DateOfCreation
            };

            return Ok(userReturnDto);
        }

        [HttpPut("users/edit/{Id}")]
        public async Task<IActionResult> EditUserAsync(int Id, [FromBody] User user)
        {
            var currentUser = await _userRepository.GetUserByIdAsync(Id);

            if (currentUser == null) 
            {
                return NotFound("User not found.");
            }

            var existingUser = await _userRepository.GetUserByUsernameEmail(user.UserName, user.Email);

            if (existingUser != null)
            {
                return Conflict("A user with this username or email already exists.");
            }

            User editedUser = await _userRepository.EditUserAsync(Id, user);

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
    }
}
