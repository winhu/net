using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.IBusiness.Core
{
    public class PermissionConfig
    {
        public string SecurityKeyName { get; set; }
        public string DynamicTokenName { get; set; }
        public string Domain { get; set; }
        public int Timeout { get; set; }

        public string WinHandleUnAuthorizedAddress { get; set; }
        //public string WinHandleProtalAddress { get; set; }
        //public string WinHandleLoginAddress { get; set; }
        //public string WinHandleLogoutAddress { get; set; }
        public string WinHandleValidatationAddress { get; set; }
        public string WinHandleUpdateInfoAddress { get; set; }
        //public string WinTmpException { get; set; }
    }
}
