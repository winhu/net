using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace System.Data.Entity
{
    internal class DataInitializer<TDbContext> : DropCreateDatabaseIfModelChanges<TDbContext> where TDbContext : DbContext
    {
        protected override void Seed(TDbContext context)
        {
            base.Seed(context);
            initializers.ForEach(item => item.Initialize(context));
        }

        private List<IDataInitializer<TDbContext>> initializers = new List<IDataInitializer<TDbContext>>();

        public void AddDataInitializer(IDataInitializer<TDbContext> initializer)
        {
            initializers.Add(initializer);
        }
    }
}
