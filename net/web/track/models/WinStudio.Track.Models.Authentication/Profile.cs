using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.MongoDB.Models;

namespace WinStudio.Track.Models.Authentication
{
    public class Profile : AuthBaseModel
    {
        public Profile() { LastLoginTime = DateTime.Now; }

        [StringLength(30)]
        public string Account { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public Gender? Gender { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        [StringLength(30)]
        public string NickName { get; set; }

        public DateTime LastLoginTime { get; set; }

        public virtual List<Contact> Contacts { get; set; }

        public string GetContact(ContactType type)
        {
            if (Contacts.Exists(c => c.Type == type))
                return Contacts.SingleOrDefault(c => c.Type == type).Content;
            return string.Empty;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", Account, Name, NickName, Gender);
        }
    }
}
