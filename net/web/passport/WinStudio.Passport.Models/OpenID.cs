using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Repository;
using WinStudio.Framework.EFModels;

namespace WinStudio.Passport.Models
{
    public class OpenID : Entity
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
