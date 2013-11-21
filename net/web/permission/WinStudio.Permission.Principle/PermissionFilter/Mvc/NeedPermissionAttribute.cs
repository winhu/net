using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    /// <summary>
    /// 使用该Attribute将支持根据该控制器的运行库(dll)自动生成权限树。
    /// </summary>
    public abstract class NeedWinPermissionAttribute : NeedValidPermissionAttribute
    {
        #region 权限设定及初始化
        /// <summary>
        /// 该权限点代码即当前Controller或Action的名称
        /// </summary>
        /// <param name="name">权限点名称</param>
        /// <param name="display">是否在权限树中显示</param>
        /// <param name="parent">父权限点代码</param>
        public NeedWinPermissionAttribute(string name, bool display = true, string parent = null)
        {
            Name = name;
            Parent = parent;
            Display = display;
        }

        /// <summary>
        /// 该权限点代码即当前Controller或Action的名称
        /// </summary>
        /// <param name="roles">该权限点所需的角色名称，命名格式RoleCode.RoleName|RoleCode.RoleName|RoleCode.RoleName</param>
        /// <param name="name">权限点名称</param>
        /// <param name="parent">父权限点代码</param>
        /// <param name="display">是否在权限树中显示</param>
        public NeedWinPermissionAttribute(string roles, string name, string parent = null, bool display = true)
            : this(name, display, parent)
        {
            Roles = roles;
        }
        //public string Roles { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示在树形结构下（如果父节点为false，则其所有子节点均为false）
        /// </summary>
        public bool Display { get; set; }

        /// <summary>
        /// 父级权限（规则：controller.action，默认则与物理结构一致；如没有使用.分隔符，则表示在物理controller分支内）
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// 判断是否为当前控制器的行为
        /// </summary>
        /// <param name="ctrl">当前控制器</param>
        /// <returns></returns>
        public bool IsNativeAction(string ctrl)
        {
            return ctrl == Parent || string.IsNullOrEmpty(Parent);
        }

        public List<KeyValuePair<string, string>> GetRoles()
        {
            List<KeyValuePair<string, string>> rkvl = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(Roles)) return rkvl;

            string[] rkvs = Roles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string rkv in rkvs)
            {
                string[] ri = rkv.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (ri.Length == 1)
                    rkvl.Add(new KeyValuePair<string, string>(ri[0], null));
                else
                    rkvl.Add(new KeyValuePair<string, string>(ri[0], ri[1]));
            }
            return rkvl;
        }

        #endregion

    }
}
