using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class SysModule
    {
        public SysModule()
        {
            Customers = new List<Customer>();
            Roles = new List<Role>();
            Functions = new List<Function>();
        }
        public override PermissionNodeType NodeType
        {
            get { return PermissionNodeType.Module; }
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
                    Functions.ForEach(delegate(Function t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
                    break;
                case PermissionNodeType.Customer:
                    Customers.ForEach(delegate(Customer t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
                    break;
                default: break;
            }
            return nodes.ToArray();
        }

        public override object GetVirtualNodes()
        {
            return new List<object>(){
                new { id = ( -100 - ID - (int)PermissionNodeType.Role), name = "所有角色", ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Role,type=(int)PermissionNodeType.Virtual,pid=ID, rpid = ID,isParent=true },
                new { id = ( -100 - ID - (int)PermissionNodeType.Customer), name = "所有人员", ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Customer,type=(int)PermissionNodeType.Virtual,pid=ID, rpid = ID  ,isParent=true},
                new { id = ( -100 - ID - (int)PermissionNodeType.Function), name = "所有功能",ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Function,type=(int)PermissionNodeType.Virtual,pid=ID, rpid = ID  ,isParent=true} }.ToArray();
        }

        public override object ToTreeNode(int pid)
        {
            return new { id = ID, name = Name, type = (int)NodeType, ptype = (int)this.NodeType, stype = (int)PermissionNodeType.Virtual, pid = ID, rpid = ID, isParent = true };
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]， {2}，在权限树{3}", Name, Code, (IsUsing ? "已启用" : "未启用"), (Display ? "显示" : "不显示"));
        }
        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Name)) ValidMsg = "系统名称不能为空！";
            if (string.IsNullOrEmpty(Code)) ValidMsg = "系统代码不能为空！";
            return base.IsValid();
        }
    }
}
