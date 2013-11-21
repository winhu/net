﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Permission.Principle.Mvc
{
    public class PermissionNode : IPermissionNode
    {
        public PermissionNode(PermissionNodeType type)
        {
            this.nodetype = type;
            Children = new List<IPermissionNode>();
        }

        private bool display;
        private string name;
        private PermissionNodeType nodetype;

        public string Name
        {
            get { return name; }
        }
        public string Address { get; set; }

        public PermissionNodeType NodeType
        {
            get { return nodetype; }
        }

        public bool Display
        {
            get { return display; }
        }
        public bool Using { get; set; }

        public bool Editable { get; set; }

        public List<IPermissionNode> Children { get; set; }

        public static IPermissionNode CreateNodeType(PermissionNodeType type, string name, string code, string parent, bool display, string address = null)
        {
            PermissionNode node = new PermissionNode(type);
            if (type == PermissionNodeType.Function)
                node.ParentCode = parent;
            node.name = name;
            node.Code = code;
            node.display = display;
            node.Address = address;
            return node;
        }


        public string Code { get; set; }

        public void AddChildren(IPermissionNode node)
        {
            Children.Add(node);
        }
        public string ParentCode { get; set; }

        public string GenKey(string sysauth)
        {
            return (sysauth + Address).ByPermissionEncrypter();
        }
    }
}
