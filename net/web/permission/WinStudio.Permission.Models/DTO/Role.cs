using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(RoleMetadata))]
    public partial class Role : BasePermission
    {
        public string Code { get; set; }

        public PermissionRoleType Type { get; set; }

        public int ModuleID { get; set; }
        public virtual SysModule Module { get; set; }

        public int? ParentID { get; set; }
        public virtual Role Parent { get; set; }

        public string BusiCode { get; set; }

        public bool Editalbe { get; set; }

        public virtual List<Role> Children { get; set; }

        public virtual List<Customer> Customers { get; set; }

        public virtual List<RFunc> Functions { get; set; }

    }
}
