using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;
using WinStudio.Permission.Principle.Mvc;
using WinStudio.PermissionBusiService.Impl;

namespace WinStudio.PermissionBusiService
{
    public class SubSystemBusiService : AbstractPermissionBusiService<SysModule>
    {
        public SubSystemBusiService() : this(new PermissionDbContext()) { }
        public SubSystemBusiService(PermissionDbContext context) : base(context) { }

        public ComRet GetAllUsingSusSystems()
        {
            return Result(GetAll().Where(s => s.IsUsing).ToArray());
        }

        public ComRet AutoGeneratePermission(string xmlpath, string syscode, string area)
        {
            SysModule system = Get(s => s.Code == syscode);
            if (system == null) return Result("不存在的系统！");

            DeleteSubSystemPermissionInDbContext(system.ID);

            IPermissionTree tree = new PermissionTree();
            tree.LoadXml(xmlpath);

            var roles = tree.GetNodes(Permission.Principle.Mvc.PermissionNodeType.Role);
            foreach (IPermissionNode role in roles)
            {
                system.Roles.Add(new Role { Code = role.Code, Name = role.Name, Display = role.Display, SystemID = system.ID });
            }
            ConvertToFunctions(system.Functions, tree.GetNodes(Permission.Principle.Mvc.PermissionNodeType.Function), system.ID, system.Authority);

            Save(system);
            return Result();
        }

        private void ConvertToFunctions(List<Function> funcs, List<IPermissionNode> nodes, int sysid, string sysauth)
        {
            foreach (IPermissionNode node in nodes)
            {
                Function func = new Function(node.GenKey(sysauth));
                func.Name = node.Name;
                func.Address = node.Address;
                func.Display = node.Display;
                func.SystemID = sysid;
                func.IsUsing = true;
                ConvertToFunctions(func.Children, node.Children, sysid, sysauth);
                funcs.Add(func);
            }
        }

        public void DeleteSubSystemPermissionInDbContext(int sysid)
        {
            var funcs = this.Get<PermissionDbContext>().Functions.Where(f => f.SystemID == sysid).ToList();
            var roles = this.Get<PermissionDbContext>().Roles.Where(f => f.SystemID == sysid).ToList();
            foreach (var f in funcs)
            {
                this.Get<PermissionDbContext>().Functions.Remove(f);
            }
            foreach (var r in roles)
            {
                this.Get<PermissionDbContext>().Roles.Remove(r);
            }
            int counter = this.Get<PermissionDbContext>().SaveChanges();
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="syscode">系统代码</param>
        /// <param name="account">用户账号</param>
        /// <returns>返回当前系统下权限字符串：account,syscode,role1|role2,funckey1|funckey2|funckey3</returns>
        public string GetUserPermission(string sysauth, string syscode, string account)
        {
            //return null;
            RoleBusiService roleServ = new RoleBusiService();
            var rs = roleServ.GetAll(r => r.System.Code == syscode && r.System.Authority == sysauth && r.Customers.Any(c => c.Account == account)).ToList();

            if (rs == null || rs.Count == 0) return string.Empty;
            List<string> roles = new List<string>();
            List<string> funcs = new List<string>();
            rs.ForEach(delegate(Role role)
            {
                roles.Add(role.CBC);
                role.CollectFunctionKeys(funcs);
            });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3},{4}", sysauth, syscode, account, roles.Join('|'), funcs.Join('|'));
            return sb.ToString();
        }
    }
}
