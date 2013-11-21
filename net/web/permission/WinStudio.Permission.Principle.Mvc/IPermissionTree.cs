using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    public interface IPermissionTree
    {
        string Name { get; }
        void SetName(string name);

        void AddPermissionNode(IPermissionNode node);
        void PickupPieces();

        IPermissionNode GetNode(string code);
        List<IPermissionNode> GetNodes(PermissionNodeType type);
        void ToXml(string path);
        void LoadXml(string path);
    }

}
