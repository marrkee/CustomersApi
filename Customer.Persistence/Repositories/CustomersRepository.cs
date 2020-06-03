namespace Customers.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Customers.Persistence.Context;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CustomersRepository : ICustomerRepository
    {
        private readonly CustomersContext context;

        public CustomersRepository(CustomersContext context)
        {
            this.context = context;
        }

        public async ValueTask<Customer> GetCustomer(int customerId)
        {
            return await this.context.Customers.FindAsync(customerId).ConfigureAwait(false);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await this.context.Customers.ToListAsync();
        }

        public async Task CreateCustomer(string customerName, int managerId)
        {
            var customer = new Customer()
            {
                CustomerName = customerName,
                ManagerId = managerId,
            };
            await this.context.Customers.AddAsync(customer).ConfigureAwait(false);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = this.context.Customers.Find(customerId);
            this.context.Customers.Remove(customer);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
