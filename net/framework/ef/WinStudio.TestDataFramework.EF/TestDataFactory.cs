using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.TestDataFramework.EF
{
    internal class TestDataFactory<TDbContext> : DropCreateDatabaseIfModelChanges<TDbContext> where TDbContext : DbContext
    {
        List<ITestDataInitializer<TDbContext>> initializers = new List<ITestDataInitializer<TDbContext>>();
        public void AddTestDataInitializer(ITestDataInitializer<TDbContext> initializer)
        {
            var init = initializers.SingleOrDefault(i => i.Code == initializer.Code);
            if (init == null || init.Order > initializer.Order)
            {
                initializers.RemoveAll(i => i.Code == initializer.Code);
                initializers.Add(initializer);
            }
        }

        protected override void Seed(TDbContext context)
        {
            base.Seed(context);
            initializers.Sort(SortTsetDataInitializer);
            foreach (var initializer in initializers)
            {
                initializer.Initialize(context);
                initializer.Save(context);
            }
        }

        private static int SortTsetDataInitializer(ITestDataInitializer<TDbContext> a, ITestDataInitializer<TDbContext> b)
        {
            return a.Order - b.Order;
        }
    }
}
