using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacAutoResolver;
using WinStudio.ComResult;

namespace WinStudio.iTrip.Core.AbstractServices
{
    public class iTripBusiServ
    {
        private static IAutofacResolver _resolver = new AutofacResolver();
        protected T GetService<T>()
        {
            return _resolver.GetService<T>();
        }
        protected ComRet Result() { return new ComRet(); }
        protected ComRet Result(bool ret, string msg = null) { return new ComRet(ret, msg); }
        protected ComRet Result(string err) { return new ComRet(err); }
        protected ComRet Result(object obj) { return new ComRet(obj); }
        protected ComRet Result(int num) { return new ComRet(num); }
        protected ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        //protected ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        //protected ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }

    }
}
