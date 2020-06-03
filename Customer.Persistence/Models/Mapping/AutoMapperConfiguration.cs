namespace Customers.Persistence.Models.Mapping
{
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration ConfigureForWeb()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WebProfile>();
            });
        }
    }
}
