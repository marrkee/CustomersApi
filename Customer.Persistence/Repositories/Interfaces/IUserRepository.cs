namespace Customers.Persistence.Repositories.Interfaces
{
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;

    public interface IUserRepository
    {
        ValueTask<User> GetUser(string username);

        Task WriteToken(User user, string token);
    }
}
