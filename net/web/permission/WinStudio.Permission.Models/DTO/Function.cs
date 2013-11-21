using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(FunctionMetadata))]
    public partial class Function : BasePermission
    {
        public int? ParentID { get; set; }
        public virtual Function Parent { get; set; }

        public virtual List<Function> Children { get; set; }

        public virtual List<Role> Roles { get; set; }

        public int? SysModuleID { get; set; }

        public virtual string Address { get; set; }

        public string Key { get; private set; }

        public bool Editable { get; set; }

    }
}
