using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace System.Data.Entity
{
    public interface IDataInitializer<TDbContext> where TDbContext : DbContext
    {
        void Initialize(TDbContext context);
    }
    internal interface IDataInitFactory<TDbContext> where TDbContext : DbContext
    {
        void Run();
    }
}
