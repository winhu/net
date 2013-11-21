using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class NeedHandleExceptionAttribute : HandleMvcExceptionAttribute
    {
        public NeedHandleExceptionAttribute()
            : base("Exception", "Index", "Exception")
        {

        }

        public override void Log(Exception e)
        {
            //e.LogError("NeedHandleExceptionAttribute");
        }
    }
}
