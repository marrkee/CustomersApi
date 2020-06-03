namespace Customers.Persistence.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Customers.Persistence.Context;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Helpers;
    using Customers.Persistence.Repositories.Interfaces;

    public class ManagersRepository : IManagersRepository
    {
        private readonly CustomersContext context;
        private readonly IMapper mapper;

        public ManagersRepository(CustomersContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Manager> GetManager(int managerId)
        {
            return await this.context.Managers.FindAsync(managerId).ConfigureAwait(false);
        }

        public async Task<PagedList<ManagersViewModel>> GetManagers(ManagersPagingParameters managersPagingParameters)
        {
            var list = await PagedList<ManagersViewModel>.ToPagedList(
                this.context.Managers.Where(m => m.Id >= managersPagingParameters.MinId).OrderBy(m => m.Id).ProjectTo<ManagersViewModel>(this.mapper.ConfigurationProvider),
                managersPagingParameters.PageNumber,
                managersPagingParameters.PageSize);
            return list;
        }

        public async Task CreateManager(Manager manager)
        {
            this.context.Managers.Add(manager);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateManager(Manager manager)
        {
            var managerToUpdate = this.context.Managers.Find(manager.Id);
            managerToUpdate.FirstName = manager.FirstName;
            managerToUpdate.LastName = manager.LastName;
            await this.context.SaveChangesAsync();
        }
    }
}
