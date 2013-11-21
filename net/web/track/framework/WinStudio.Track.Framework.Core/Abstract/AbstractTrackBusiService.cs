using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Framework.Web.Services;
using WinStudio.MongoDB.Models;
using WinStudio.MongoDB.Repository;
using WinStudio.Track.Models;
using WinStudio.Track.Framework.Core.Abstract;
using AutofacAutoResolver;

namespace WinStudio.Track.Framework.Core
{
    public abstract class AbstractTrackBusiService<T> : MongoRepository<T> where T : MongoModel
    {
        public ComRet Result() { return new ComRet(); }
        public ComRet Result(bool ret, string msg) { return new ComRet(ret, msg); }
        public ComRet Result(string err) { return new ComRet(err); }
        public ComRet Result(object obj) { return new ComRet(obj); }
        public ComRet Result(int num) { return new ComRet(num); }
        public ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        public ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        public ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }

        static IAutofacResolver _resolver = new AutofacResolver();
        public TInterface GetService<TInterface>()
        {
            if (typeof(TInterface).IsInterface)
                return _resolver.GetService<TInterface>();
            return default(TInterface);
        }
    }
    public abstract class AbstractAuthBusiService<T> : MongoRepository<T> where T : MongoModel
    {
        public ComRet Result() { return new ComRet(); }
        public ComRet Result(bool ret, string msg) { return new ComRet(ret, msg); }
        public ComRet Result(string err) { return new ComRet(err); }
        public ComRet Result(object obj) { return new ComRet(obj); }
        public ComRet Result(int num) { return new ComRet(num); }
        public ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        public ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        public ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }
    }
}
