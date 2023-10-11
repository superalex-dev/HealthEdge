using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResponseModel>> Login([FromBody] UserLogin user)
        {
            AuthenticatedResponseModel isLoginSuccessfull = await this._authenticationRepository.Login(user);

            if (isLoginSuccessfull != null)
            {
                return Ok(isLoginSuccessfull);
            }

            return Unauthorized();
        }
    }
}
