using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Models.SystemUser
{
    public class Contact : BaseModel
    {
        public int ProfileID { get; set; }

        public ContactType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }
    }
}
