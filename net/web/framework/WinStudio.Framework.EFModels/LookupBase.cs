using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.EFModels
{
    public class LookupBase : BaseModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(128)]
        public string Value { get; set; }
    }
}
