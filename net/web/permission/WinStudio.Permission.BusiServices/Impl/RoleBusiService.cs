using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Impl
{
    [Export(typeof(IRoleBusiService))]
    public class RoleBusiService : AbstractPermissionBusiService<Role>, IRoleBusiService
    {
        public ComRet SaveRole(Role role)
        {
            return Result(Save(role));
        }

        public ComRet GetRoles(int mid)
        {
            return Result(GetAll(r => r.ModuleID == mid).ToList());
        }


        public ComRet GetRole(int mid, int pid, int id)
        {
            if (id <= 0 && mid > 0)
            {
                var r = new Role();
                r.ModuleID = mid;
                return Result(r);
            }

            if (id <= 0 && pid > 0)
            {
                Role role = GetById(pid);
                var r = new Role();
                r.ModuleID = role.ModuleID;
                r.ParentID = role.ID;
                return Result(r);
            }
            if (id > 0) return GetRole(id);
            return Result(false);
        }

        public ComRet GetRole(int id) { return Result(GetById(id)); }


        public ComRet GetPermissions(string moduleauth, string modulecode, string custaccount)
        {
            return Result(GetAll(r => r.Module.Code == modulecode && r.Module.Authority == moduleauth && r.Customers.Any(c => c.Account == custaccount)).ToList());
        }



        public ComRet CopyFunctions(int id, string funcs, List<Function> all)
        {
            try
            {
                if (string.IsNullOrEmpty(funcs) || all == null || all.Count == 0) return Result("没有功能点可以复制！");

                Role role = GetById(id);
                if (role == null) return Result("角色不存在！");
                List<RFunc> ret = new List<RFunc>();
                List<int> ids = new List<int>();
                funcs.Split(new char[] { '|', '_', '.' }, StringSplitOptions.RemoveEmptyEntries).ForEach(i => ids.Add(i.ToInt(0)));

                CopyFunctionTree(all, ret, ids.ToArray(), false, role.ID);
                if (role.Functions.Count > 0)
                {
                    role.Functions.Clear();
                    Save(role);
                }
                role.Functions = ret; ;
                return Result(Save(role));
            }
            catch (Exception e)
            {
                return Result(e.Message);
            }
        }

        private int CopyFunctionTree(List<Function> all, List<RFunc> ret, int[] ids, bool include, int rid = 0)
        {
            int counter = 0;
            foreach (Function func in all)
            {
                if (include)
                {
                    counter++;
                    RFunc rfunc = new RFunc(func);
                    if (rid > 0)
                        rfunc.RoleID = rid;
                    ret.Add(rfunc);
                    counter += CopyFunctionTree(func.Children, rfunc.Children, ids, include, 0);
                }
                else if (ids.Contains(func.ID))
                {
                    counter++;
                    RFunc rfunc = new RFunc(func);
                    if (rid > 0)
                        rfunc.RoleID = rid;
                    ret.Add(rfunc);
                    var can = func.IsAllChildrenCanBeCopied(ids);
                    counter += CopyFunctionTree(func.Children, rfunc.Children, ids, can, 0);
                }
            }
            return counter;
        }
    }
}
