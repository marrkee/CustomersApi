namespace Customers.Persistence.Models.Mapping
{
    using AutoMapper;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Helpers;

    public class WebProfile : Profile
    {
        public WebProfile()
        {
            this.CreateMap<Customer, CustomersViewModel>();
            this.CreateMap<CustomersViewModel, Customer>();
            this.CreateMap<Manager, ManagersViewModel>();
            this.CreateMap<ManagersViewModel, Manager>();
            this.CreateMap<PagedList<ManagersViewModel>, PagedList<Manager>>();
            this.CreateMap<PagedList<Manager>, PagedList<ManagersViewModel>>();
        }
    }
}
