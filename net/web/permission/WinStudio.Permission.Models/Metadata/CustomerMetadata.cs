using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class CustomerMetadata
    {
        [Required]
        [Display(Name = "账号")]
        [StringLength(30, MinimumLength = 6)]
        public string Account { get; set; }
    }
}
