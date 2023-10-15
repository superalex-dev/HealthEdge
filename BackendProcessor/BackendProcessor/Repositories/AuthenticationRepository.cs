using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendProcessor.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly HospitalDbContext context;

        public AuthenticationRepository(DbContextOptions<HospitalDbContext> options)
        {
            context = new HospitalDbContext(options);
        }

        public async Task<AuthenticatedResponseModel> Login(UserLogin user)
        {
            User doesUserExist = await context.Users
                .Where(u => u.Email == user.Email && u.Password == user.Password)
                .FirstOrDefaultAsync();

            if (doesUserExist != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7147",
                    audience: "https://localhost:7147",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new AuthenticatedResponseModel { Token = tokenString };
            }

            return null;
        }
    }
}
