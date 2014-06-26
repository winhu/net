using System;

namespace System
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    public class WinWcfServiceAttribute : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Interface)]
    public class WinWcfIServiceAttribute : Attribute
    {
    }
}
