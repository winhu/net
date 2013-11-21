using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    public interface ICommonService
    {
        string GenerateToken(string flag);
    }

    public class CommonService : ICommonService
    {
        public string GenerateToken(string flag = null)
        {
            return Convert.ToBase64String(flag == null ? Guid.NewGuid().ToByteArray() : Encoding.UTF8.GetBytes(flag + DateTime.Now.ToString("yyyyMMddHHmmssffffff")));
        }
    }
}
