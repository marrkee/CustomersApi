namespace Customers.Persistence.Repositories.Interfaces
{
    using System.Threading.Tasks;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Helpers;

    public interface IManagersService
    {
        Task<Manager> GetManager(int managerId);

        Task<PagedList<ManagersViewModel>> GetManagers(ManagersPagingParameters managersPagingParameters);

        Task CreateManager(Manager manager);

        Task UpdateManager(Manager manager);
    }
}
