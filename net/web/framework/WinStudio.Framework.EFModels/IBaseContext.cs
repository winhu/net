using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.EFModels
{
    public interface IDbContext
    {
        int Commit();
    }

    public class WinDbContext : DbContext, IDbContext
    {
        public WinDbContext() { }
        public WinDbContext(string connstr)
            : base(connstr)
        { }
        public int Commit()
        {
            return this.SaveChanges();
        }
    }
}
