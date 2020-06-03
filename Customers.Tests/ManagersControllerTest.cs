namespace Customers.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.Mapping;
    using Customers.Persistence.Repositories;
    using Customers.Persistence.Services;
    using Customers.WebApi.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class ManagersControllerTest
    {
        [Fact]
        public async Task TestGetManagersAsync()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestGetManagersAsync));
            var managersRepository = new ManagersRepository(dbContext, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var managersService = new ManagersService(managersRepository, null);
            var controller = new ManagersController(managersService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper())
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext(),
                },
            };
            var parameters = new ManagersPagingParameters()
            {
                PageNumber = 1,
                PageSize = 1,
                MinId = 0,
            };
            var response = await controller.GetManagers(parameters);
            var value = response.Value;

            Assert.Single(value);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            };
            parameters = new ManagersPagingParameters()
            {
                PageNumber = 1,
                PageSize = 2,
                MinId = 0,
            };
            response = await controller.GetManagers(parameters);
            value = response.Value;
            Assert.Equal(2, value.Count());
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            };
            parameters = new ManagersPagingParameters()
            {
                PageNumber = 1,
                PageSize = 2,
                MinId = 2,
            };
            response = await controller.GetManagers(parameters);
            value = response.Value;
            Assert.Single(value);

            dbContext.Dispose();
        }

        [Fact]
        public async Task TestGetManagerAsync()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestGetManagerAsync));
            var managersRepository = new ManagersRepository(dbContext, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var managersService = new ManagersService(managersRepository, null);
            var controller = new ManagersController(managersService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var response = await controller.GetManager(2);
            var value = response.Value;

            dbContext.Dispose();

            Assert.Equal("Test", value.FirstName);
        }

        [Fact]
        public async void TestCreateManager()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestCreateManager));
            var managersRepository = new ManagersRepository(dbContext, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var managersService = new ManagersService(managersRepository, null);
            var controller = new ManagersController(managersService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var parameters = new ManagersViewModel()
            {
                FirstName = "john",
                LastName = "Shon",
            };
            var response = await controller.CreateNewManager(parameters);
            Assert.Equal(1, dbContext.Managers.Where(c => c.LastName == "Shon").Count());

            dbContext.Dispose();
        }

        [Fact]
        public async void TestUpdateManagersAsync()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestUpdateManagersAsync));
            var managersRepository = new ManagersRepository(dbContext, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var managersService = new ManagersService(managersRepository, null);
            var controller = new ManagersController(managersService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var parameters = new JObject()
            {
                new JProperty("customerName", "test"),
                new JProperty("managerId", "1"),
            };

            var manager = dbContext.Managers.FirstOrDefault();

            var updatewith = new ManagersViewModel()
            {
                Id = manager.Id,
                FirstName = "johnTEST",
                LastName = "Shon",
            };

            var response = await controller.UpdateManager(updatewith);
            Assert.Equal(updatewith.FirstName, dbContext.Managers.Find(manager.Id).FirstName);

            dbContext.Dispose();
        }
    }
}