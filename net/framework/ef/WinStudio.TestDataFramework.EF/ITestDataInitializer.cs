using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.TestDataFramework.EF
{
    public interface ITestDataInitializer<TDbContext> where TDbContext : DbContext
    {
        void Initialize(TDbContext context);

        string Code { get; }

        int Order { get; }

        int Save(TDbContext context);
    }


    public interface ITestDataInitializer
    {
        void Initialize<TDbContext>(TDbContext context) where TDbContext : DbContext;

        string Code { get; }

        int Order { get; }

        int Save();
    }
}
