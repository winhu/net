using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;

namespace WinStudio.PermissionBusiService
{
    public class PermissionTreeBusiService
    {

        SubSystemBusiService systemServ = new SubSystemBusiService();
        RoleBusiService roleServ = new RoleBusiService();
        FunctionBusiService funcServ = new FunctionBusiService();

        public object GetPermissionTreeRoot(bool editalbe)
        {
            var systems = editalbe ? systemServ.GetAll() : systemServ.GetAll(s => s.IsUsing);
            List<object> nodes = new List<object>();
            //systems.ToList().ForEach(s => nodes.Add(s.GetVirtualNodes()));
            return nodes.ToArray();
        }

        public object GetPermissionNodes(int id = 0, int type = 0, int ptype = -1)
        {
            PermissionNodeType nodetype = type.ToEnum<PermissionNodeType>();
            PermissionNodeType nodeptype = ptype.ToEnum<PermissionNodeType>();

            if (id == 0 && nodetype == PermissionNodeType.RegSystem)
            {
                List<object> nodes = new List<object>();
                systemServ.GetAll().ToList().ForEach(s => nodes.Add(s.ToTreeNode(id)));
                return nodes.ToArray();
            }
            if (id > 0)
            {
                ITreeNode node = null;
                if (nodeptype == PermissionNodeType.RegSystem)
                {
                    node = systemServ.GetById(id);
                }
                else if (nodeptype == PermissionNodeType.Role)
                {
                    node = roleServ.GetById(id);
                }
                else if (nodeptype == PermissionNodeType.Function)
                {
                    node = funcServ.GetById(id);
                }
                if (node == null) return null;
                return node.GetChildrenNodes(nodetype, id);
            }
            return null;
        }
    }
}
