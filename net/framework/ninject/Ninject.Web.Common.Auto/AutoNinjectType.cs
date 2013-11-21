using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Web.Common.Auto
{
    /// <summary>
    /// AutoNinjectType: interface and implementation
    /// </summary>
    internal class AutoNinjectType
    {
        /// <summary>
        /// implementation
        /// </summary>
        public Type ImplService { get; set; }

        /// <summary>
        /// interfaces
        /// </summary>
        public Type[] IServices { get; set; }
    }
}
