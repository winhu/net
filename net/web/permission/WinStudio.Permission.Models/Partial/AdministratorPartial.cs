using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class Administrator
    {
        public Administrator()
        {
            LastLoginTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Account);
        }
    }
}
