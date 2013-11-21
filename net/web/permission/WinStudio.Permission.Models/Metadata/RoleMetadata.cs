using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class RoleMetadata
    {
        [Required]
        [StringLength(30)]
        [Display(Name = "角色代码")]
        public string Code { get; set; }

        [StringLength(20)]
        [Display(Name = "业务代码")]
        public string BusiCode { get; set; }

        [Display(Name = "可编辑")]
        public bool Editalbe { get; set; }

    }
}
