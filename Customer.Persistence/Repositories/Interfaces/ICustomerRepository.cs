namespace Customers.Persistence.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;

    public interface ICustomerRepository
    {
        ValueTask<Customer> GetCustomer(int customerId);

        Task<List<Customer>> GetCustomers();

        Task CreateCustomer(string customerName, int managerId);

        Task DeleteCustomer(int customerId);
    }
}
