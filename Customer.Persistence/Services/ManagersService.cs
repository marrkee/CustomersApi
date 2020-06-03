namespace Customers.Persistence.Services
{
    using System;
    using System.Threading.Tasks;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Helpers;
    using Customers.Persistence.Repositories.Interfaces;
    using Microsoft.Extensions.Logging;

    public class ManagersService : IManagersService
    {
        private readonly IManagersRepository managersRepository;
        private readonly ILogger<ManagersService> logger;

        public ManagersService(IManagersRepository managersRepository, ILogger<ManagersService> logger)
        {
            this.managersRepository = managersRepository;
            this.logger = logger;
        }

        public async Task<Manager> GetManager(int managerId)
        {
            try
            {
                return await this.managersRepository.GetManager(managerId);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error retrieving manager" + ex);
                throw;
            }
        }

        public async Task<PagedList<ManagersViewModel>> GetManagers(ManagersPagingParameters managersPagingParameters)
        {
            try
            {
                return await this.managersRepository.GetManagers(managersPagingParameters);
        }
                        catch (Exception ex)
            {
                this.logger.LogError("Error retrieving managers" + ex);
                throw;
            }
        }

        public async Task CreateManager(Manager manager)
        {
            try
            {
                await this.managersRepository.CreateManager(manager);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error creating manager" + ex);
                throw;
            }
        }

        public async Task UpdateManager(Manager manager)
        {
            try
            {
                await this.managersRepository.UpdateManager(manager);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error updating manager" + ex);
                throw;
            }
        }
    }
}
