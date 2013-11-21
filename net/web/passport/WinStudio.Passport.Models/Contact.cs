using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.Framework.EFModels;

namespace WinStudio.Passport.Models
{
    public class Contact : Entity
    {
        public string ProfileID { get; set; }

        public ContactType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }
    }
}
