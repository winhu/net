using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Integration.Mvc.Auto
{
    /// <summary>
    /// AutofacType: interface and implementation
    /// </summary>
    public class AutofacType
    {
        public AutofacType(Type impl, Type[] iserv)
        {
            this.impl = impl;
            this.iserv = iserv;
        }
        private Type impl;
        private Type[] iserv;
        /// <summary>
        /// implementation
        /// </summary>
        public Type ImplService { get { return impl; } }

        /// <summary>
        /// interfaces
        /// </summary>
        public Type[] IServices { get { return iserv; } }
    }
}
