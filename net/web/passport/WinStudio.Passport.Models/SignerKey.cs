using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Repository;
using WinStudio.Framework.EFModels;

namespace WinStudio.Passport.Models
{
    public class SignerKey : RefEntity
    {
        public SignerKey() { LastTime = DateTime.Now; }

        [StringLength(30)]
        [Key]
        [ScaffoldColumn(false)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime LastTime { get; set; }
    }
}
