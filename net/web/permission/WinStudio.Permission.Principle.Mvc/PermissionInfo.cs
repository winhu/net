using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    internal delegate PermissionInfo GatherPermissionInfoAction(HttpContextBase context);
    public struct PermissionInfo
    {
        public string UserAccount { get; set; }
        public string SysCode { get; set; }
        public string BusiCode { get; set; }
        public bool IsLegal()
        {
            return !string.IsNullOrEmpty(UserAccount) && !string.IsNullOrEmpty(SysCode);
        }
        public bool IsLegalBusi()
        {
            return !string.IsNullOrEmpty(UserAccount) && !string.IsNullOrEmpty(SysCode) && !string.IsNullOrEmpty(BusiCode);
        }
    }
}
