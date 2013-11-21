using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public interface ITreeNode
    {
        int ID { get; }
        string Name { get; }
        PermissionNodeType NodeType { get; }
        int? PID { get; }
        int RPID { get; }
        bool Display { get; }


        object ToTreeNode(int pid);
        object GetChildrenNodes(PermissionNodeType type, PermissionNodeType ptype, int rpid);
        object GetVirtualNodes();
    }

}
