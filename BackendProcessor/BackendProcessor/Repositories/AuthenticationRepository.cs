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
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(DbContextOptions<HospitalDbContext> options, IConfiguration configuration)
        {
            context = new HospitalDbContext(options);
            _configuration = configuration;
        }

        public async Task<AuthenticatedResponseModel> Login(UserLogin user)
        {
            Patient doesPatientExist = await context.Patients
                .Where(u => u.Email == user.Email || u.UserName == user.UserName)
                .FirstOrDefaultAsync();
        
            if (doesPatientExist != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, doesPatientExist.Password);
                Console.WriteLine($"Password is valid: {isPasswordValid}");
        
                if (!isPasswordValid)
                {
                    Console.WriteLine("Passwords do not match");
                    return null;
                }
        
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
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
