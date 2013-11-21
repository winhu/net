
using System.Data.Entity;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Framework.Repository
{
    public interface IDatabaseFactory
    {
        BaseDbContext Get();
        TDbContext Get<TDbContext>() where TDbContext : BaseDbContext;
    }
}