using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Business
{
    [Serializable]
    public class UserInfo : IUserInfo
    {
        public string SecurityKey { get; set; }

        public string Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string IP { get; set; }

        public string Origin { get; set; }

        public DateTime RegisterdTime { get; set; }
    }
}
