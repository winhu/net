using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class AdministratorMetadata
    {
        [ScaffoldColumn(true)]
        [DisplayName("用户名")]
        [StringLength(20, MinimumLength = 6)]
        public string Account { get; set; }

    }
}
