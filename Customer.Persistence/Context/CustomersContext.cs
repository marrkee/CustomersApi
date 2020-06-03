namespace Customers.Persistence.Context
{
    using Customers.Persistence.Models.DataModels;
    using Microsoft.EntityFrameworkCore;

    public class CustomersContext : BaseContext, ICustomersContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<User> Users { get; set; }

        public CustomersContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
