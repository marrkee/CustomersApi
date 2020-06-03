namespace Customers.Persistence.Models.DataModels
{
    using Customers.Persistence.Models.DataModels.BaseClasses;

    public class Manager : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
