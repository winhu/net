using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Integration.Mvc.AutoAttributed
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UseAutofacAttribute : Attribute
    {
    }
}
