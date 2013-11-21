using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WinStudio.TestDataFramework.EF
{
    public class TestDataRuner<TDbContext> where TDbContext : DbContext
    {
        public virtual List<ITestDataInitializer<TDbContext>> GetTestDataInitializers()
        {
            var types = Utility.GetAssembly<ITestDataInitializer<TDbContext>>().FindTypes<ITestDataInitializer<TDbContext>>();
            List<ITestDataInitializer<TDbContext>> lst = new List<ITestDataInitializer<TDbContext>>();
            foreach (Type type in types)
            {
                ITestDataInitializer<TDbContext> init = Activator.CreateInstance(type) as ITestDataInitializer<TDbContext>;
                lst.Add(init);
            }
            return lst;
        }

        public void Run()
        {
            TestDataFactory<TDbContext> factory = new TestDataFactory<TDbContext>();
            foreach (ITestDataInitializer<TDbContext> initializer in GetTestDataInitializers())
            {
                factory.AddTestDataInitializer(initializer);
            }
            Database.SetInitializer(factory);
        }
    }
}
