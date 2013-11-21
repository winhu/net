using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.MongoDB.Models;

namespace WinStudio.Track.Models.Authentication
{
    public class Contact : AuthBaseModel
    {
        public int ProfileID { get; set; }

        public ContactType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }
    }
}
