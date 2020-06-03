namespace Customers.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Customers.Persistence.Models.Mapping;
    using Customers.Persistence.Repositories;
    using Customers.Persistence.Services;
    using Customers.WebApi.Controllers;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class CustomersControllerTest
    {
        [Fact]
        public async Task TestGetCustomersAsync()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestGetCustomersAsync));
            var customersRepository = new CustomersRepository(dbContext);
            var customerService = new CustomersService(customersRepository, null);
            var controller = new CustomersController(customerService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var response = await controller.GetCustomers();
            var value = response.Value;
            Assert.Equal(dbContext.Customers.Count(), value.Count());

            dbContext.Dispose();
        }

        [Fact]
        public async Task TestGetCustomerAsync()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestGetCustomerAsync));
            var customersRepository = new CustomersRepository(dbContext);
            var customerService = new CustomersService(customersRepository, null);
            var controller = new CustomersController(customerService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());
            var response = await controller.GetCustomer(1);
            var value = response.Value;

            dbContext.Dispose();

            Assert.Equal("John Johnson", value.CustomerName);
        }

        [Fact]
        public async void TestAddCustomer()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestAddCustomer));
            var customersRepository = new CustomersRepository(dbContext);
            var customerService = new CustomersService(customersRepository, null);
            var controller = new CustomersController(customerService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var parameters = new JObject()
            {
                new JProperty("customerName", "test"),
                new JProperty("managerId", "1"),
            };
            var response = await controller.CreateNewCustomer(parameters);
            Assert.Equal(1, dbContext.Customers.Where(c => c.CustomerName == "test").Count());

            dbContext.Dispose();
        }

        [Fact]
        public async void TestDeleteCustomer()
        {
            var dbContext = DbContextMocker.GetContext(nameof(this.TestDeleteCustomer));
            var customersRepository = new CustomersRepository(dbContext);
            var customerService = new CustomersService(customersRepository, null);
            var controller = new CustomersController(customerService, AutoMapperConfiguration.ConfigureForWeb().CreateMapper());

            var parameters = new JObject()
            {
                new JProperty("customerName", "test"),
                new JProperty("managerId", "1"),
            };
            Assert.NotNull(dbContext.Customers.Find(2));
            var response = await controller.DeleteCustomer(2);
            Assert.Null(dbContext.Customers.Find(2));

            dbContext.Dispose();
        }
    }
}