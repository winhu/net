using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.AssemblyUtility;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Generator
{
    public class FuncTree
    {
        public FuncTree(string area)
        {
            Functions = new List<Func>();
            Area = area;
        }
        public string Area { get; set; }
        List<Func> Functions { get; set; }
        public Func GetFunction(string parent)
        {
            if (string.IsNullOrEmpty(parent)) return null;
            if (parent.Contains('.'))
            {
                var ca = parent.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return GetFunction(Functions, ca[0], ca[1]);
            }
            return GetFunction(Functions, parent, null);
        }
        public Func GetFunction(List<Func> funcs, string ctrl, string action)
        {
            if (funcs == null) return null;
            var ret = funcs.SingleOrDefault(f => f.Controller == ctrl && f.Action == action);
            if (ret == null)
            {
                foreach (var func in funcs)
                {
                    var f = GetFunction(func.Sons, ctrl, action);
                    if (f != null) return f;
                }
            } return ret;
        }
        public void AddFunction(string name, string ctrl, bool isnode = true)
        {
            Functions.Add(new Func(name, Area, ctrl, isnode));
        }
        public List<Func> GetFunctions() { return Functions; }

        List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
        public void AddRoles(List<KeyValuePair<string, string>> roles)
        {
            if (roles == null || roles.Count == 0) return;
            foreach (var role in roles)
            {
                if (!this.roles.Exists(r => r.Key == role.Key))
                    this.roles.Add(role);
                if (this.roles.Exists(r => r.Key == role.Key && r.Value == null && role.Value != null))
                {
                    this.roles.RemoveAll(r => r.Key == role.Key && r.Value == null);
                    this.roles.Add(role);
                }
            }
        }
        public List<Function> ToFunctions(int sysid)
        {
            List<Function> fs = new List<Function>();
            Functions.ForEach(f => fs.Add(f.ToFunction(null, sysid)));
            return fs;
        }
        public List<Role> ToRoles(int sysid)
        {
            List<Role> roles = new List<Role>();
            this.roles.ForEach(r => roles.Add(new Role() { SystemID = sysid, Code = r.Key, Name = r.Value, Display = true, Editalbe = false, IsUsing = true }));
            return roles;
        }
        public int CountFunc { get { return Functions.Count; } }
    }

    public class Func : Function
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ParentCode { get; set; }
        private string address = string.Empty;
        public List<Func> Sons { get; set; }
        public Func(string name, string area, string ctrl, bool isnode = true)
        {
            Name = name;
            Area = area;
            Controller = ctrl;
            Display = isnode;
            Sons = new List<Func>();
        }
        public Func(string name, string area, string ctrl, string action, bool isnode = true)
        {
            Name = name;
            Area = area;
            Controller = ctrl;
            Action = action;
            Display = isnode;
            Sons = new List<Func>();
        }
        public Func(string name, string area, string ctrl, string action, string parent, bool isnode = true)
        {
            Name = name;
            Area = area;
            Controller = ctrl;
            Action = action;
            Display = isnode;
            ParentCode = parent;
            Sons = new List<Func>();
        }
        public void AddChildren(Func child)
        {
            if (!Sons.Exists(c => c.Area == child.Area && c.Controller == child.Controller && c.Action == child.Action))
            {
                if (!Display) child.Display = false;
                Sons.Add(child);
            }
        }
        public void AddChildren(string name, string action, string ctrl = null, bool isnode = true)
        {
            Sons.Add(new Func(name, Area, ctrl ?? Controller, action, this.Display ? isnode : Display));
        }

        public Function ToFunction(Function parent, int sysid)
        {
            string add = string.IsNullOrEmpty(Area) ? string.Format("/{0}/{1}", Controller, Action) : string.Format("{0}/{1}/{2}", Area, Controller, Action);
            Function f = new Function(PermissionUtility.PermissionEncrypter(add));
            f.Name = Name;
            f.SystemID = sysid;
            f.Address = add;// string.Format("{0}/{1}/{2}", Area, Controller, Action);
            f.Display = Display;
            f.IsMvcFramework = true;
            f.IsUsing = true;
            f.Parent = parent;
            foreach (Func s in Sons)
            {
                f.Children.Add(s.ToFunction(f, sysid));
            }
            return f;
        }
    }

    public class Generator
    {

        public FuncTree Run(string assemblyname, string area)
        {
            var ass = Utility.GetAssembly(assemblyname);
            return Run(ass, area);
        }
        /// <summary>
        /// 根据程序集和域得到所有功能点
        /// </summary>
        /// <param name="assemblyname">程序集名称</param>
        /// <param name="area">域名称</param>
        /// <returns></returns>
        public FuncTree Run(Assembly assemblies, string area)
        {
            {
                var tree = new FuncTree(area);
                try
                {
                    var funcs = new List<Func>();
                    foreach (var type in assemblies.FindTypes<NeedWinPermissionAttribute>())
                    {
                        NeedWinPermissionAttribute cattr = type.GetCustomAttribute<NeedWinPermissionAttribute>(true);
                        string fname = cattr.Name;
                        string ctrl = type.Name.Replace("Controller", "");
                        tree.AddFunction(fname, ctrl, cattr.Display);
                        foreach (var method in type.FindMethods<NeedWinPermissionAttribute>())
                        {
                            NeedWinPermissionAttribute aattr = method.GetCustomAttribute<NeedWinPermissionAttribute>(true);
                            tree.AddRoles(aattr.GetRoles());
                            if (!aattr.IsNativeAction(ctrl))
                            {
                                if (null == tree.GetFunction(aattr.Parent))
                                {
                                    funcs.Add(new Func(aattr.Name, area, ctrl, method.Name, aattr.Parent, aattr.Display));
                                    continue;
                                }
                                else tree.GetFunction(aattr.Parent).AddChildren(new Func(aattr.Name, area, ctrl, method.Name, aattr.Parent, aattr.Display));
                            }
                            else tree.GetFunction(aattr.IsNativeAction(ctrl) ? ctrl : aattr.Parent).AddChildren(aattr.Name, method.Name, ctrl, aattr.Display);
                        }
                    }
                    funcs.Reverse();
                    foreach (var f in funcs)
                    {
                        Func parent = tree.GetFunction(f.ParentCode);
                        if (parent != null) parent.AddChildren(f);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return tree;
            }
        }
    }
}
