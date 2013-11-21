using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.Web.Services;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Impl
{
    public class PermissionTreeBusiService : AbstractPermissionBusiService<Role>
    {

        SysModuleBusiService systemServ = new SysModuleBusiService();
        RoleBusiService roleServ = new RoleBusiService();
        FunctionBusiService funcServ = new FunctionBusiService();

        public object GetPermissionTreeRoot(bool editalbe)
        {
            var systems = editalbe ? systemServ.GetAll() : systemServ.GetAll(s => s.IsUsing);
            List<object> nodes = new List<object>();
            //systems.ToList().ForEach(s => nodes.Add(s.GetVirtualNodes()));
            return nodes.ToArray();
        }
        ISysModuleBusiService ibsModule;
        IRoleBusiService ibsRole;
        IFunctionBusiService ibsFunction;
        IRFunctionBusiService ibsRFunction;
        public object GetPermissionNodes(int id = 0, int pid = 0, int type = 0, int ptype = -1, int stype = -1)
        {
            if (id == 0 && pid == 0 &&
                type == (int)PermissionNodeType.Virtual &&
                ptype == (int)PermissionNodeType.Virtual &&
                stype == (int)PermissionNodeType.Module)
            {
                List<object> nodes = new List<object>();
                systemServ.GetAll().ToList().ForEach(s => nodes.Add(s.ToTreeNode(pid)));
                return nodes.ToArray();
            }
            if (pid > 0)
            {
                if (type == (int)PermissionNodeType.Module && stype == (int)PermissionNodeType.Virtual)
                {
                    ibsModule = GetService<ISysModuleBusiService>();
                    var ret = ibsModule.GetSysModule(pid);
                    if (!ret.Ret) return null;
                    return ret.Instance<SysModule>().GetVirtualNodes();
                }
                else if (type == (int)PermissionNodeType.Role && stype == (int)PermissionNodeType.Virtual)
                {
                    ibsRole = GetService<IRoleBusiService>();
                    var ret = ibsRole.GetRole(pid);
                    if (!ret.Ret) return null;
                    return ret.Instance<Role>().GetVirtualNodes();
                }
                if (type == (int)PermissionNodeType.Virtual && stype == (int)PermissionNodeType.Role)
                {
                    if (ptype == (int)PermissionNodeType.Module)
                    {
                        ibsModule = GetService<ISysModuleBusiService>();
                        var ret = ibsModule.GetSysModule(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<SysModule>().GetChildrenNodes(PermissionNodeType.Role, PermissionNodeType.Module, pid);
                    }
                    else if (ptype == (int)PermissionNodeType.Role)
                    {
                        ibsRole = GetService<IRoleBusiService>();
                        var ret = ibsRole.GetRole(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<Role>().GetChildrenNodes(PermissionNodeType.Role, PermissionNodeType.Virtual, pid);
                    }
                }
                else if (type == (int)PermissionNodeType.Virtual && stype == (int)PermissionNodeType.Customer)
                {
                    if (ptype == (int)PermissionNodeType.Module)
                    {
                        ibsModule = GetService<ISysModuleBusiService>();
                        var ret = ibsModule.GetSysModule(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<SysModule>().GetChildrenNodes(PermissionNodeType.Customer, PermissionNodeType.Module, pid);
                    }
                    if (ptype == (int)PermissionNodeType.Role)
                    {
                        ibsRole = GetService<IRoleBusiService>();
                        var ret = ibsRole.GetRole(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<Role>().GetChildrenNodes(PermissionNodeType.Customer, PermissionNodeType.Role, pid);
                    }
                }
                else if (type == (int)PermissionNodeType.Virtual && stype == (int)PermissionNodeType.Function)
                {
                    if (ptype == (int)PermissionNodeType.Module)
                    {
                        ibsModule = GetService<ISysModuleBusiService>();
                        var ret = ibsModule.GetSysModule(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<SysModule>().GetChildrenNodes(PermissionNodeType.Function, PermissionNodeType.Module, pid);
                    }
                    if (ptype == (int)PermissionNodeType.Role)
                    {
                        ibsRole = GetService<IRoleBusiService>();
                        var ret = ibsRole.GetRole(pid);
                        if (!ret.Ret) return null;
                        return ret.Instance<Role>().GetChildrenNodes(PermissionNodeType.Function, PermissionNodeType.Role, pid);
                    }
                }
                else if (type == (int)PermissionNodeType.Function && stype == (int)PermissionNodeType.Function)
                {
                    ibsFunction = GetService<IFunctionBusiService>();
                    var ret = ibsFunction.GetFunction(pid);
                    if (!ret.Ret) return null;
                    return ret.Instance<Function>().GetChildrenNodes(PermissionNodeType.Function, PermissionNodeType.Function, pid);
                }
                else if (type == (int)PermissionNodeType.RFunction)
                {
                    ibsRFunction = GetService<IRFunctionBusiService>();
                    var ret = ibsRFunction.GetFunction(id);
                    if (!ret.Ret) return null;
                    return ret.Instance<RFunc>().GetChildrenNodes(PermissionNodeType.RFunction, PermissionNodeType.RFunction, pid);
                }

            }


            //PermissionNodeType nodetype = type.ToEnum<PermissionNodeType>();
            //PermissionNodeType nodeptype = ptype.ToEnum<PermissionNodeType>();

            //if (id == 0 && nodetype == PermissionNodeType.Module)
            //{
            //    List<object> nodes = new List<object>();
            //    systemServ.GetAll().ToList().ForEach(s => nodes.Add(s.ToTreeNode(id)));
            //    return nodes.ToArray();
            //}
            //if (id > 0)
            //{
            //    ITreeNode node = null;
            //    if (nodeptype == PermissionNodeType.Module)
            //    {
            //        node = systemServ.GetById(id);
            //    }
            //    else if (nodeptype == PermissionNodeType.Role)
            //    {
            //        node = roleServ.GetById(id);
            //    }
            //    else if (nodeptype == PermissionNodeType.Function)
            //    {
            //        node = funcServ.GetById(id);
            //    }
            //    if (node == null) return null;
            //    return node.GetChildrenNodes(nodetype, nodeptype, id);
            //}
            return null;
        }
    }
}
