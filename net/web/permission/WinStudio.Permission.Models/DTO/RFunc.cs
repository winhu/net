using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(RFunc))]
    public partial class RFunc : BasePermission
    {
        public int ParentID { get; set; }
        public virtual RFunc Parent { get; set; }

        [ForeignKey("ParentID")]
        public virtual List<RFunc> Children { get; set; }

        public int? RoleID { get; set; }

        public int FunctionID { get; set; }

        public virtual string Address { get; set; }

        public string Key { get; private set; }

    }
}
