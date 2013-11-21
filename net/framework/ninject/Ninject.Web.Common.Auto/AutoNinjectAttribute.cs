using System;

namespace Ninject.Web.Common.Auto
{
    /// <summary>
    /// AutoNinjectAttribute: it is necessary, used only for class, interface and assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly)]
    public class AutoNinjectAttribute : Attribute
    {
        public bool IsSingleton { get; set; }
        public string Memo { get; set; }
    }
}
