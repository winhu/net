using System;
using System.ComponentModel.DataAnnotations;
using WinStudio.MongoDB.Models;

namespace WinStudio.Track.Models.Authentication
{
    public class SignerKey : AuthBaseModel
    {
        public SignerKey() { LastTime = DateTime.Now; }
        
        [StringLength(30)]
        [ScaffoldColumn(false)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime LastTime { get; set; }
    }
}
