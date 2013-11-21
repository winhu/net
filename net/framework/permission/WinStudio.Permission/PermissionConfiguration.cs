using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    public class PermissionConfiguration
    {
        public string WinTokenName { get; set; }
        public string WinTokenDomain { get; set; }
        public int WinTokenTimeout { get; set; }

        public string WinHandleUnAuthorizedAddress { get; set; }
        //public string WinHandleProtalAddress { get; set; }
        //public string WinHandleLoginAddress { get; set; }
        //public string WinHandleLogoutAddress { get; set; }
        public string WinHandleValidatationAddress { get; set; }
        public string WinHandleUpdateInfoAddress { get; set; }
        //public string WinTmpException { get; set; }
    }
}
