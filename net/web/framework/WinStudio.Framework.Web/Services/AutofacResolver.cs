using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace WinStudio.Framework.Web.Services
{
    public class AutofacResolver
    {
        internal static IContainer AutofacBusiContainer;
        public static void SetAutofacBusiContainer(IContainer container)
        {
            AutofacBusiContainer = container;
        }
        public static T GetService<T>()
        {
            if (AutofacBusiContainer == null) return default(T);
            return AutofacBusiContainer.Resolve<T>();
        }
    }
}
