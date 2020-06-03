namespace Customers.Persistence.Repositories
{
    using System.Threading.Tasks;
    using Customers.Persistence.Context;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly CustomersContext context;

        public UserRepository(CustomersContext context)
        {
            this.context = context;
        }

        public async ValueTask<User> GetUser(string username)
        {
            return await this.context.Users.FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
        }

        public async Task WriteToken(User user, string token)
        {
            user.Token = token;
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
