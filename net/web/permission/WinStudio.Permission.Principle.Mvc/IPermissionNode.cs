using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    public interface IPermissionNode
    {
        string Name { get; }
        string Code { get; set; }
        string ParentCode { get; set; }
        PermissionNodeType NodeType { get; }
        bool Display { get; }
        bool Using { get; set; }
        bool Editable { get; set; }
        string Address { get; set; }

        string GenKey(string sysauth);

        List<IPermissionNode> Children { get; set; }
        void AddChildren(IPermissionNode node);
    }

}
