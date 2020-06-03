namespace Customers.Persistence.Models.DataModels.BaseClasses
{
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBaseWithoutAudit
    {
        [Key]
        public int Id { get; set; }
    }
}
