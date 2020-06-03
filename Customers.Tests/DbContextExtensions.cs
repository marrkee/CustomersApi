namespace Customers.Tests
{
    using Customers.Persistence.Context;
    using Customers.Persistence.Models.DataModels;
    using Microsoft.AspNetCore.Identity;

    public static class DbContextExtensions
    {
        public static void Seed(this CustomersContext dbContext)
        {
            dbContext.Customers.Add(new Customer()
            {
                ManagerId = 1,
                CustomerName = "John Johnson",
            });

            dbContext.Customers.Add(new Customer()
            {
                ManagerId = 6,
                CustomerName = "Adam Smitson",
            });

            dbContext.Managers.Add(new Manager()
            {
                FirstName = "Victor",
                LastName = "Smitson",
            });
            dbContext.Managers.Add(new Manager()
            {
                FirstName = "Test",
                LastName = "Smitson",
            });

            var passwordHash = new PasswordHasher<User>();
            var user = new User()
            {
                Username = "test",
                Password = "test",
                Role = "Administrator",
            };
            user.Password = passwordHash.HashPassword(user, "test");

            dbContext.Users.Add(user);

            dbContext.SaveChanges();
        }
    }
}