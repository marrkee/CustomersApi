namespace Customers.Tests
{
    using System.Threading.Tasks;
    using Customers.Persistence.Models;
    using Customers.Persistence.Repositories;
    using Customers.Persistence.Services;
    using Customers.WebApi.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Xunit;

    public class UsersControllerTest
    {
        [Fact]
        public async Task TestLogin()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestLogin));
            var usersRepository = new UserRepository(dbContext);
            var usersService = new UserService(usersRepository, null);
            var appsettings = Options.Create(new WebApi.Configuration.AppSettings()
            {
                Secret = "TESTSUPERSECRETKEYINTESTING",
            });
            var controller = new UsersController(usersService, appsettings)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext(),
                },
            };

            var userLogin = new UserLoginModel()
            {
                Username = "test",
                Password = "test",
            };

            var response = await controller.Authenticate(userLogin);
            var value = response.Value;
            Assert.Equal("Administrator", value.Role);

            userLogin = new UserLoginModel()
            {
                Username = "test",
                Password = "testas",
            };

            response = await controller.Authenticate(userLogin);
            value = response.Value;
            Assert.Null(value);
        }
    }
}