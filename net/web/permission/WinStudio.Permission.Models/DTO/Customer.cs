using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer : BasePermission
    {
        public string Account { get; set; }

        public virtual List<SysModule> SysModules { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}
