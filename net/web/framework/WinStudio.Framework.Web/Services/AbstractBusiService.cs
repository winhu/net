using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacAutoResolver;
using WinStudio.ComResult;
using WinStudio.Framework.EFModels;
using WinStudio.Framework.Web.Repository;

namespace WinStudio.Framework.Web.Services
{

    public abstract class AbstractBusiService<T> : RepositoryBase<T> where T : BaseModel
    {
        public AbstractBusiService(WinDbContext context) : base(context) { }
        public AbstractBusiService(IDatabaseFactory factory) : base(factory) { }

        public ComRet Result() { return new ComRet(); }
        public ComRet Result(bool ret, string msg = null) { return new ComRet(ret, msg); }
        public ComRet Result(string err) { return new ComRet(err); }
        public ComRet Result(object obj) { return new ComRet(obj); }
        public ComRet Result(int num) { return new ComRet(num); }
        public ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        public ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        public ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }

        //protected TInterface GetService<TInterface>()
        //{
        //    return AutofacResolver.GetService<TInterface>();
        //}

        static IAutofacResolver _resolver = new AutofacResolver();
        protected TInterface GetService<TInterface>()
        {
            return _resolver.GetService<TInterface>();
        }
        public WinDbContext GetContext() { return this.Get(); }
        public void SetContext(WinDbContext context)
        {
            this.DatabaseFactory.Set(context);
        }

    }
    public abstract class AbstractAuthBusiService<T> : RepositoryBase<T> where T : BaseModel
    {
        public AbstractAuthBusiService(WinDbContext context) : base(context) { }
        public AbstractAuthBusiService(IDatabaseFactory factory) : base(factory) { }

        public ComRet Result() { return new ComRet(); }
        public ComRet Result(bool ret, string msg = null) { return new ComRet(ret, msg); }
        public ComRet Result(string err) { return new ComRet(err); }
        public ComRet Result(object obj) { return new ComRet(obj); }
        public ComRet Result(int num) { return new ComRet(num); }
        public ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        public ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        public ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }
    }
}
