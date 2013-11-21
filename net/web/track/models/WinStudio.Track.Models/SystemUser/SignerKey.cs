using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;

namespace WinStudio.Track.Models.SystemUser
{
    public class SignerKey : BaseModel
    {
        public SignerKey() { LastTime = DateTime.Now; }

        [NotMapped]
        public override int ID { get; set; }

        [StringLength(30)]
        [Key]
        [ScaffoldColumn(false)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime LastTime { get; set; }
    }
}
