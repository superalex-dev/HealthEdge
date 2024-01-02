using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository { get; set; }

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
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            User createdUser = await _userRepository.CreateUserAsync(user);

            if (createdUser == null)
            {
                return BadRequest();
            }

            return Ok(createdUser);
        }

        [HttpPut("users/edit/{Id}")]
        public async Task<IActionResult> EditUserAsync(int Id, [FromBody] User user)
        {
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
