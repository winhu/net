using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class FunctionMetadata
    {
        [Required]
        public virtual string Address { get; set; }

        [Required]
        [StringLength(256)]
        public string Key { get; private set; }
    }
}
