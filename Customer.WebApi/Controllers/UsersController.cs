namespace Customers.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Services.Interfaces;
    using Customers.WebApi.Configuration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAppSettings appSettings;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate([FromBody] UserLoginModel userInput)
        {
            var user = await this.userService.Authenticate(userInput.Username, userInput.Password, this.appSettings.Secret);

            if (user == null)
            {
                return this.Unauthorized(new { message = "Username or password is incorrect" });
            }

            return user;
        }
    }
}