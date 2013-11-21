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
    public class UserSignHistory : Entity
    {
        [StringLength(30)]
        public string Account { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        public DateTime CreatedTime { get; set; }
    }

    public class ModuleSignHistory : Entity
    {
        [StringLength(20)]
        public string Code { get; set; }

        public int Counter { get; set; }

        public DateTime LastDate { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
