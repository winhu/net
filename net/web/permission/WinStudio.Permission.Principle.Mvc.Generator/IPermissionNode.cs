using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    public interface IPermissionNode
    {
        int ID { get; }
        string Name { get; }
        string Code { get; set; }
        string ParentCode { get; set; }
        PermissionNodeType NodeType { get; }
        int? ParentID { get; }
        int RPID { get; }
        bool Display { get; }
        bool Using { get; set; }
        bool Editable { get; set; }
        string Address { get; set; }

        List<IPermissionNode> Children { get; set; }
        void AddChildren(IPermissionNode node);

        object ToTreeNode(int pid);
        object GetChildrenNodes(PermissionNodeType type, int rpid);
        object GetVirtualNodes();
    }

}
