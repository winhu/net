using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(SysModuleMetadata))]
    public partial class SysModule : BasePermission
    {
        public string Code { get; set; }

        public string Authority { get; set; }

        public virtual List<Customer> Customers { get; set; }

        public virtual List<Role> Roles { get; set; }

        public virtual List<Function> Functions { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

    }

}
