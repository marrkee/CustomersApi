namespace Customers.Persistence.Models.DataModels.BaseClasses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBase : EntityBaseWithoutAudit
    {
        /// <summary>
        /// Gets or sets timestamp of creation of the entity.
        /// </summary>
        [Required]
        public DateTime CreatedWhen { get; set; }

        /// <summary>
        /// Gets or sets timestamp of entity update.
        /// </summary>
        [Required]
        public DateTime UpdatedWhen { get; set; }
    }
}