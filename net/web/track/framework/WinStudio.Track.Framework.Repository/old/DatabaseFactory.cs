
using System.Data.Entity;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Framework.Repository
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private BaseDbContext _dataContext;

        public BaseDbContext Get()
        {
            return _dataContext;//?? (_dataContext = new DbContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }


        public TDbContext Get<TDbContext>() where TDbContext : BaseDbContext
        {
            return _dataContext as TDbContext;//?? (_dataContext = new TDbContext());
        }
    }
}