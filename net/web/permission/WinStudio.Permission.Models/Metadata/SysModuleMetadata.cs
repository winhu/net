using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class SysModuleMetadata
    {
        [Required]
        [Display(Name = "模块代码")]
        [StringLength(30)]
        public string Code { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "域名")]
        [DataType(DataType.Url)]
        public string Authority { get; set; }
    }
}
