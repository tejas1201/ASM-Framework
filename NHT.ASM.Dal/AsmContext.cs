using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHT.ASM.Dal
{
    public class AsmContext : DbContext
    {
        public AsmContext() : base("name=AsmContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AsmContext>());
            Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges<AsmContext>());
        }

        public override int SaveChanges()
        {

            var count = ChangeTracker.Entries().Count(x => x.State == EntityState.Added);
            var result = base.SaveChanges();
            if (result < count)
            {
                throw new ArgumentException($"Saved to few entries: {result} instead of {count}");
            }

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
