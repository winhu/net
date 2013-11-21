
using System.Data.Entity;
using WinStudio.Framework.EFModels;

namespace WinStudio.Framework.Web.Repository
{
    public interface IDatabaseFactory
    {
        WinDbContext Get();
        TDbContext Get<TDbContext>() where TDbContext : WinDbContext;
        void Set(WinDbContext context);
        void Set<TDbContext>(TDbContext context) where TDbContext : WinDbContext;
    }
}