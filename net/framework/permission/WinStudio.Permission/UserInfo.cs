using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    [Serializable]
    public class UserInfo
    {
        public string Token { get; set; }

        public string Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Module { get; set; }

        public DateTime LastTime { get; set; }

        public string Host { get; set; }
    }
}
