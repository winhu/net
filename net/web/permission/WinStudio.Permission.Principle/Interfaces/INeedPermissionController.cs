using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Interfaces
{
    public interface INeedPermissionController
    {
        string Account { get; }

        /// <summary>
        /// 业务代码
        /// </summary>
        string BusiCode { get; }

        /// <summary>
        /// 当前系统域标识
        /// </summary>
        string SysAuth { get; }

        string Key { get; }
    }
}
