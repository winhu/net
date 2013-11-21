using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Models
{
    public class OpenAuthentication : BaseModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Uri { get; set; }
    }
}
