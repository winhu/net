using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public enum PermissionNodeType
    {
        Virtual = 0,
        Module = 1,
        Role = 2,
        Function = 3,
        Customer = 4,
        RFunction = 5
    }
    public enum PermissionRoleType
    {
        [Description("业务管理员")]
        BusiAdmin = 0,
        [Description("系统管理员")]
        SystemAdmin = 10,
        [Description("超级管理员")]
        SuperAdmin = 100
    }

    public enum OperationType
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
    }
}
