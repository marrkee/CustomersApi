namespace Customers.Persistence.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Interfaces;
    using Customers.Persistence.Services.Interfaces;
    using Microsoft.Extensions.Logging;

    public class CustomersService : ICustomersService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(ICustomerRepository customerRepositor, ILogger<CustomersService> logger)
        {
            this.customerRepository = customerRepositor;
            this.logger = logger;
        }

        public async ValueTask<Customer> GetCustomer(int customerId)
        {
            try
            {
                var customer = await this.customerRepository.GetCustomer(customerId);
                return customer;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error retriving customer" + ex);
                throw;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                var customers = await this.customerRepository.GetCustomers();
                return customers;
        }
            catch (Exception ex)
            {
                this.logger.LogError("Error retriving customers" + ex);
                throw;
            }
}

        public async Task CreateCustomer(string customerName, int managerId)
        {
            try
            {
                await this.customerRepository.CreateCustomer(customerName, managerId);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error creating customer" + ex);
                throw;
            }
        }

        public async Task DeleteCustomer(int customerId)
        {
            try
            {
                await this.customerRepository.DeleteCustomer(customerId);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error deleting customer" + ex);
                throw;
            }
        }
    }
}
