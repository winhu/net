using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace System.Data.Entity
{
    internal class DataInitializerFactory<TDbContext> : DropCreateDatabaseIfModelChanges<TDbContext> where TDbContext : DbContext
    {
        protected override void Seed(TDbContext context)
        {
            base.Seed(context);
            foreach (IDataInitializer<TDbContext> initializer in initializers)
            {
                initializer.Initialize(context);
            }
        }

        private List<IDataInitializer<TDbContext>> initializers = new List<IDataInitializer<TDbContext>>();

        internal void AddDataInitializer(IDataInitializer<TDbContext> initializer)
        {
            initializers.Add(initializer);
        }
    }
}
