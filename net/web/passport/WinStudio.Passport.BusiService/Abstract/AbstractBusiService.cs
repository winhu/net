using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.ComResult;
using WinStudio.Framework.EFModels;
using WinStudio.Framework.Web.Repository;
using WinStudio.Framework.Web.Services;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.BusiService.Abstract
{
    public abstract class AbstractPassportBusiService<T> where T : Entity
    {
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
    }
}
