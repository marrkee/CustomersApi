namespace Customers.Persistence.Context
{
    using System;
    using Customers.Persistence.Models.DataModels;
    using Microsoft.EntityFrameworkCore;

    public interface ICustomersContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Manager> Managers { get; set; }

        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
