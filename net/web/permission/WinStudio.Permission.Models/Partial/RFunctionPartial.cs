using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public partial class RFunc
    {
        /// <summary>
        /// 初始化可编辑功能点
        /// </summary>
        public RFunc()
        {
            Children = new List<RFunc>();
            IsUsing = true;
        }
        /// <summary>
        /// 初始化不可编辑功能点
        /// </summary>
        /// <param name="key"></param>
        public RFunc(string key)
        {
            Key = key;
            Children = new List<RFunc>();
            IsUsing = true;
        }

        public RFunc(Function func)
        {
            FunctionID = func.ID;
            Name = func.Name;
            Key = func.Key;
            Address = func.Address;
            Children = new List<RFunc>();
        }
        public override PermissionNodeType NodeType
        {
            get { return PermissionNodeType.RFunction; }
        }
        public void SetKey(string key)
        {
            Key = key;
        }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Address))
            {
                ValidMsg = "功能地址不能为空！";
                return false;
            }
            return base.IsValid();
        }
        public override object GetChildrenNodes(PermissionNodeType type, PermissionNodeType ptype, int rpid)
        {
            if (type == ptype)
                return GetVirtualNodes();
            List<object> nodes = new List<object>();
            //switch (type)
            //{
            //    case PermissionNodeType.Role:
            //        Roles.ForEach(delegate(Role t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
            //        break;
            //    case PermissionNodeType.Function:
            //        Children.ForEach(delegate(Function t) { if (t.Display)nodes.Add(t.ToTreeNode(ID)); });
            //        break;
            //    default: break;
            //}
            return nodes.ToArray();
        }

        public override object GetVirtualNodes()
        {

            List<object> nodes = new List<object>();
            Children.ForEach(r => nodes.Add(r.ToTreeNode(ID)));
            return nodes.ToArray();
        }

        public override object ToTreeNode(int pid)
        {
            return new { id = ID, name = Name, type = (int)NodeType, ptype = Parent != null ? (int)PermissionNodeType.RFunction : (int)PermissionNodeType.Virtual, stype = (int)PermissionNodeType.Function, pid = pid, rpid = ID, isParent = Children.Count > 0 };
        }
    }
}
