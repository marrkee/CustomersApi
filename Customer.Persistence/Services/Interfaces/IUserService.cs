namespace Customers.Persistence.Services.Interfaces
{
    using System.Threading.Tasks;
    using Customers.Persistence.Models.DataModels;

    public interface IUserService
    {
        Task<User> Authenticate(string username, string password, string secret);
    }
}
