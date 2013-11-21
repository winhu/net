using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MongoDB.Repository;
using WinStudio.Framework.EFModels;

namespace WinStudio.Passport.Models
{
    public class Profile : RefEntity
    {
        public Profile() { LastLoginTime = DateTime.Now; }

        [StringLength(30)]
        public string Account { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string NickName { get; set; }

        public Gender? Gender { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime LastLoginTime { get; set; }
        

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", Account, Name, NickName, Gender);
        }
    }
}
