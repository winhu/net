using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class HistoryMetadata
    {
        [Required]
        [StringLength(128)]
        public string ModelType { get; set; }

        [StringLength(256)]
        public string Detail { get; set; }
    }
}
