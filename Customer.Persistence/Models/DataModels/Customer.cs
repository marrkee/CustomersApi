namespace Customers.Persistence.Models.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using Customers.Persistence.Models.DataModels.BaseClasses;

    public class Customer : EntityBaseWithoutAudit
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public int ManagerId { get; set; }
    }
}
