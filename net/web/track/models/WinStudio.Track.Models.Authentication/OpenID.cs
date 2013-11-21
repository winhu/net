using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.MongoDB.Models;

namespace WinStudio.Track.Models.Authentication
{
    public class OpenID : AuthBaseModel
    {
        [StringLength(30)]
        public string Account { get; set; }

        [Required]
        [StringLength(256)]
        public string OpenAccount { get; set; }

        [Required]
        [StringLength(256)]
        public string OpenTicket { get; set; }

        [Required]
        [StringLength(30)]
        public string Type { get; set; }
    }
}
