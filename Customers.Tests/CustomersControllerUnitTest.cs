namespace Customers.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Models.Mapping;
    using Customers.Persistence.Repositories;
    using Customers.Persistence.Services;
    using Customers.Persistence.Services.Interfaces;
    using Customers.WebApi.Controllers;
    using Moq;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class CustomersControllerUnitTest
    {
        [Fact]
        public async Task TestGetCustomersUnitAsync()
        {
            var customerService = new Mock<ICustomersService>();
            customerService.Setup(repo => repo.GetCustomers()).ReturnsAsync(new List<Customer>() { new Customer() });
            var controller = new CustomersController(customerService.Object, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var response = await controller.GetCustomers();
            var value = response.Value;
            Assert.Single(value);
        }

        [Fact]
        public async Task TestGetCustomerUnitAsync()
        {
            var customerService = new Mock<ICustomersService>();
            customerService.Setup(repo => repo.GetCustomer(It.IsAny<int>())).ReturnsAsync(new Customer() { CustomerName = "Test" });
            var controller = new CustomersController(customerService.Object, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var response = await controller.GetCustomer(1);
            var value = response.Value;
            Assert.NotNull(value);
        }

        [Fact]
        public async void TestAddCustomer()
        {
            var customerService = new Mock<ICustomersService>();
            customerService.Setup(repo => repo.CreateCustomer(It.IsAny<string>(), It.IsAny<int>()));
            var controller = new CustomersController(customerService.Object, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var parameters = new JObject()
            {
                new JProperty("customerName", "test"),
                new JProperty("managerId", "1"),
            };

            var response = await controller.CreateNewCustomer(parameters);
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkResult", response.GetType().ToString());
        }

        [Fact]
        public async void TestDeleteCustomer()
        {
            var customerService = new Mock<ICustomersService>();
            customerService.Setup(repo => repo.DeleteCustomer(It.IsAny<int>()));
            var controller = new CustomersController(customerService.Object, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var response = await controller.DeleteCustomer(1);
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkResult", response.GetType().ToString());
        }
    }
}