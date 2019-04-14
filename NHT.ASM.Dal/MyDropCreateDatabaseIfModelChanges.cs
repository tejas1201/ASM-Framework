using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using NHT.ASM.Dal.Helpers;

namespace NHT.ASM.Dal
{
    public class MyDropCreateDatabaseIfModelChanges<TContext> : DropCreateDatabaseIfModelChanges<TContext> where TContext : DbContext
    {
        protected override void Seed(TContext context)
        {
            SeedHelper.DoSeed(context);

            base.Seed(context);
        }
    }

    public class MyDropCreateDatabaseAlways<TContext> : DropCreateDatabaseAlways<TContext> where TContext : DbContext
    {
        protected override void Seed(TContext context)
        {
            SeedHelper.DoSeed(context);

            base.Seed(context);
        }
    }
}
