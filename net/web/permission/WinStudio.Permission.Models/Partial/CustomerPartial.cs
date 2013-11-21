using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class Customer
    {
        public Customer() { }
        public Customer(int mid) { ModuleID = mid; }

        public override PermissionNodeType NodeType
        {
            get { return PermissionNodeType.Customer; }
        }
        public override object GetChildrenNodes(PermissionNodeType type, PermissionNodeType ptype, int rpid)
        {
            if (type == ptype)
                return GetVirtualNodes();
            List<object> nodes = new List<object>();
            switch (type)
            {
                case PermissionNodeType.Role:
                    Roles.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
                    break;
                //case Models.TreeNodeType.Function:
                //    Children.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
                //    break;
                default: break;
            }
            return nodes.ToArray();
        }

        public override object GetVirtualNodes()
        {
            return new List<object>(){
                new { id = 100 - (int)PermissionNodeType.Role, name = "所有角色", ptype=(int)this.NodeType,type = (int)PermissionNodeType.Role,rpid=ID, pid = ID ,isParent=true},
                new { id = 100 - (int)PermissionNodeType.Function, name = "所有功能", ptype=(int)this.NodeType,type = (int)PermissionNodeType.Function,rpid=ID, pid = ID ,isParent=true } }.ToArray();
        }

        [NotMapped]
        public int ModuleID { get; set; }

        public Customer SetModule(int mid)
        {
            if (mid > 0)
                this.ModuleID = mid;
            return this;
        }
    }
}
