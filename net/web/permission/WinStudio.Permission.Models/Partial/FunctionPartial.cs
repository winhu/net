using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class Function
    {
        /// <summary>
        /// 初始化可编辑功能点
        /// </summary>
        public Function()
        {
            Children = new List<Function>();
            Roles = new List<Role>();
            Editable = true;
            IsUsing = true;
        }
        /// <summary>
        /// 初始化不可编辑功能点
        /// </summary>
        /// <param name="key"></param>
        public Function(string key)
        {
            Key = key;
            Children = new List<Function>();
            Roles = new List<Role>();
            IsUsing = true;
        }
        public override PermissionNodeType NodeType
        {
            get { return PermissionNodeType.Function; }
        }
        public void SetKey(string key)
        {
            Key = key;
        }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Address))
            {
                ValidMsg = "功能地址不能为空！";
                return false;
            }
            return base.IsValid();
        }
        public override object GetChildrenNodes(PermissionNodeType type, PermissionNodeType ptype, int rpid)
        {
            if (type == ptype)
                return GetVirtualNodes();
            List<object> nodes = new List<object>();
            switch (type)
            {
                case PermissionNodeType.Role:
                    Roles.ForEach(delegate(Role t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
                    break;
                case PermissionNodeType.Function:
                    Children.ForEach(delegate(Function t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
                    break;
                default: break;
            }
            return nodes.ToArray();
        }

        public override object GetVirtualNodes()
        {

            List<object> nodes = new List<object>();
            Children.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
            return nodes.ToArray();
            return new List<object>(){
                new { id = 100 - (int)PermissionNodeType.Module, name = "所有角色", ptype=(int)this.NodeType,type = (int)PermissionNodeType.Module, pid = ID },
                new { id = 100 - (int)PermissionNodeType.Function, name = "所有功能",ptype=(int)this.NodeType, type = (int)PermissionNodeType.Function, pid = ID  } }.ToArray();
        }

        public override object ToTreeNode(int pid)
        {
            return new { id = ID, name = Name, type = (int)NodeType, ptype = ParentID > 0 ? (int)PermissionNodeType.Function : (int)PermissionNodeType.Virtual, stype = (int)PermissionNodeType.Function, pid = pid, rpid = ID, isParent = Children.Count > 0 };
        }

        public bool IsAllChildrenCanBeCopied(int[] ids)
        {
            foreach (var func in Children)
                if (ids.Contains(func.ID)) return false;
            return true;
        }
    }
}
