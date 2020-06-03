namespace Customers.Persistence.Context
{
    using System;
    using System.Linq;
    using Customers.Persistence.Models.DataModels.BaseClasses;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseContext : DbContext
    {
        protected BaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            try
            {
                var addedAuditedEntities = this.ChangeTracker.Entries<EntityBase>()
                    .Where(p => p.State == EntityState.Added)
                    .Select(p => p.Entity);

                var modifiedAuditedEntities = this.ChangeTracker.Entries<EntityBase>()
                    .Where(p => p.State == EntityState.Modified)
                    .Select(p => p.Entity);

                var now = DateTime.Now;

                foreach (var added in addedAuditedEntities)
                {
                    added.CreatedWhen = now;
                    added.UpdatedWhen = now;
                }

                foreach (var modified in modifiedAuditedEntities)
                {
                    modified.UpdatedWhen = now;
                }

                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                // Throw a new DbEntityValidationException with the improved exception message.
                throw new Exception(ex.ToString());
            }
        }
    }
}
