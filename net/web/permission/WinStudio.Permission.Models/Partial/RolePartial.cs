using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class Role
    {
        public Role()
        {
            Children = new List<Role>();
            Customers = new List<Customer>();
            Functions = new List<RFunc>();
            this.Type = PermissionRoleType.BusiAdmin;
        }
        public override PermissionNodeType NodeType
        {
            get { return PermissionNodeType.Role; }
        }
        public string CBC
        {
            get
            {
                return string.IsNullOrEmpty(BusiCode) ? Code : Code + "." + BusiCode;
            }
        }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Name)) ValidMsg = "角色名称不能为空！";
            if (ModuleID <= 0) ValidMsg = "必须为该角色指定系统！";
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
                    Children.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
                    break;
                case PermissionNodeType.Function:
                    Functions.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
                    break;
                case PermissionNodeType.Customer:
                    Customers.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
                    break;
                default: break;
            }
            return nodes.ToArray();
        }

        public override object GetVirtualNodes()
        {
            return new List<object>(){
                new { id = ( -100 - ID - (int)PermissionNodeType.Module), name = "所有角色", ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Role,type = (int)PermissionNodeType.Virtual,rpid = ID, pid = ID,isParent=true },
                new { id = ( -100 - ID - (int)PermissionNodeType.Customer), name = "所有人员", ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Customer,type = (int)PermissionNodeType.Virtual,rpid = ID, pid = ID ,isParent=true },
                new { id = ( -100 - ID - (int)PermissionNodeType.Function), name = "所有功能", ptype=(int)this.NodeType, stype = (int)PermissionNodeType.Function,type = (int)PermissionNodeType.Virtual,rpid = ID, pid = ID ,isParent=true } }.ToArray();
        }

        public override object ToTreeNode(int pid)
        {
            return new { id = ID, name = Name, type = (int)NodeType, ptype = (int)this.NodeType, stype = (int)PermissionNodeType.Virtual, pid = pid, rpid = ID, isParent = true };
        }

        public void CollectFunctionKeys(List<string> FuncKeys)
        {
            FunctionKeyString(Functions, FuncKeys);
        }

        private void FunctionKeyString(List<RFunc> funcs, List<string> FuncKeys)
        {
            foreach (RFunc f in funcs)
            {
                if (!FuncKeys.Contains(f.Key))
                    FuncKeys.Add(f.Key);
                FunctionKeyString(f.Children, FuncKeys);
            }
        }

    }
}
