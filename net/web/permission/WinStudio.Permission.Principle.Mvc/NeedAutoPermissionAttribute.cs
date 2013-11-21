using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    /// <summary>
    /// 权限验证：使用该Attribute将支持根据该控制器的运行库(dll)自动生成权限树。
    /// </summary>
    public abstract class NeedAutoPermissionAttribute : NeedValidPermissionAttribute
    {
        #region 权限设定及初始化

        /// <summary>
        /// 该权限点代码即当前Controller或Action的名称
        /// </summary>
        /// <param name="name">权限点名称</param>
        /// <param name="display">是否在权限树中显示</param>
        /// <param name="parent">父权限点代码</param>
        //public NeedAutoPermissionAttribute(string name, bool display = true, string parent = null)
        //    : base(true)
        //{
        //    this.name = name;
        //    this.parent = parent;
        //    this.display = display;
        //}

        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称
        ///// </summary>
        ///// <param name="roles">该权限点所需的角色名称，命名格式RoleCode.RoleName|RoleCode.RoleName|RoleCode.RoleName</param>
        ///// <param name="name">权限点名称</param>
        ///// <param name="parent">父权限点代码</param>
        ///// <param name="display">是否在权限树中显示</param>
        //public NeedAutoPermissionAttribute(string roles, string name, string parent = null, bool display = true)
        //    : this(name, display, parent)
        //{
        //    Roles = roles;
        //}

        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称（权限点名称为代码名，父权限点依据代码结构产生，无角色限制，默认显示）
        ///// </summary>
        ///// <param name="inheritPermission">是否继承父权限</param>
        public NeedAutoPermissionAttribute(bool inheritPermission = true)
            : this(string.Empty, string.Empty, string.Empty, true, inheritPermission) { }
        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称（父权限点依据代码结构产生，无角色限制，默认显示）
        ///// </summary>
        ///// <param name="name">权限点名称</param>
        ///// <param name="inheritPermission">是否继承父权限</param>
        public NeedAutoPermissionAttribute(string name, bool inheritPermission = true)
            : this(string.Empty, name, string.Empty, true, inheritPermission) { }
        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称（无角色限制，默认显示）
        ///// </summary>
        ///// <param name="name">权限点名称</param>
        ///// <param name="parent">父权限点代码</param>
        ///// <param name="inheritPermission">是否继承父权限</param>
        public NeedAutoPermissionAttribute(string name, string parent, bool inheritPermission = true)
            : this(string.Empty, name, parent, true, inheritPermission) { }
        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称（默认显示）
        ///// </summary>
        ///// <param name="roles">该权限点所需的角色名称，命名格式RoleCode.RoleName|RoleCode.RoleName|RoleCode.RoleName</param>
        ///// <param name="name">权限点名称</param>
        ///// <param name="parent">父权限点代码</param>
        ///// <param name="inheritPermission">是否继承父权限</param>
        public NeedAutoPermissionAttribute(string roles, string name, string parent, bool inheritPermission = true)
            : this(roles, name, parent, true, inheritPermission) { }
        ///// <summary>
        ///// 该权限点代码即当前Controller或Action的名称
        ///// </summary>
        ///// <param name="roles">该权限点所需的角色名称，命名格式RoleCode.RoleName|RoleCode.RoleName|RoleCode.RoleName</param>
        ///// <param name="name">权限点名称</param>
        ///// <param name="parent">父权限点代码</param>
        ///// <param name="display">是否在权限树中显示</param>
        ///// <param name="inheritPermission">是否继承父权限</param>
        public NeedAutoPermissionAttribute(string roles, string name, string parent, bool display, bool inheritPermission = true)
            : base(inheritPermission)
        {
            Roles = roles;
            this.name = name;
            this.parent = parent;
            this.display = display;
        }
        private string name, parent;
        private bool display;
        //public string Roles { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// 是否显示在树形结构下（如果父节点为false，则其所有子节点均为false）
        /// </summary>
        public bool Display { get { return display; } set { display = value; } }

        /// <summary>
        /// 父级权限（规则：controller.action，默认则与物理结构一致；如没有使用.分隔符，则表示在物理controller分支内）
        /// </summary>
        public string Parent { get { return parent; } set { parent = value; } }


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
