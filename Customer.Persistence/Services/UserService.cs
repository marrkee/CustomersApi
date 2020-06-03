namespace Customers.Persistence.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Interfaces;
    using Customers.Persistence.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<User> Authenticate(string username, string password, string secret)
        {
            try
            {
                var passwordHash = new PasswordHasher<User>();
                var user = await this.userRepository.GetUser(username);

                if (user == null)
                {
                    return null;
                }

                if (passwordHash.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
                {
                    return null;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                await this.userRepository.WriteToken(user, user.Token);
                return user;
            }
            catch (Exception ex)
            {
                this.logger.LogError("error authenticating user" + ex);
                throw;
            }
        }
    }
}
