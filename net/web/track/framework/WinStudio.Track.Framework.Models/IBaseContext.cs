using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Track.Framework.Models
{
    public interface IDbContext
    {
        int Commit();
    }

    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext() { }
        public BaseDbContext(string connstr)
            : base(connstr)
        { }
        public int Commit()
        {
            return this.SaveChanges();
        }
    }
}
