using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Interfaces
{
    public interface IPermissionTree
    {
        string Name { get; }

        void AddPermissionNode(IPermissionNode node, string parentCode);

        IPermissionNode GetNode(int id);
        IPermissionNode GetNode(string code);
        List<IPermissionNode> GetNodes();
    }

}
