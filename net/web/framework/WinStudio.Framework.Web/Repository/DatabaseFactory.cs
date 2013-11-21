
using System.Data.Entity;
using WinStudio.Framework.EFModels;

namespace WinStudio.Framework.Web.Repository
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private WinDbContext _dataContext;

        public WinDbContext Get()
        {
            return _dataContext;//?? (_dataContext = new DbContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }


        public TDbContext Get<TDbContext>() where TDbContext : WinDbContext
        {
            return _dataContext as TDbContext;//?? (_dataContext = new TDbContext());
        }


        public void Set(WinDbContext context)
        {
            _dataContext = context;
        }

        public void Set<TDbContext>(TDbContext context) where TDbContext : WinDbContext
        {
            Set(context);
        }
    }
}