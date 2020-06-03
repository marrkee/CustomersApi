namespace Customers.Tests
{
    using Customers.Persistence.Context;
    using Microsoft.EntityFrameworkCore;

    public static class DbContextMocker
    {
        public static CustomersContext GetContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CustomersContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new CustomersContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}