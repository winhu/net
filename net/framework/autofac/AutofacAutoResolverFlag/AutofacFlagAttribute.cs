using System;

namespace System
{
    /// <summary>
    /// 标识一个类或程序集是否使用Autofac
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly)]
    public class AutofacServiceFlagAttribute : Attribute
    {
        private Type _service;
        public Type Service { get { return _service; } }
        public AutofacServiceFlagAttribute()
        { }
        public AutofacServiceFlagAttribute(Type service)
        {
            _service = service;
        }
    }
    /// <summary>
    /// 标识一个程序集是否使用Autofac
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AutofacControllerFlagAttribute : Attribute
    {
        public AutofacControllerFlagAttribute()
        { }
    }
}
