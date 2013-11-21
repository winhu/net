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
    public class Module : RefEntity
    {
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public bool Status { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        [StringLength(50)]
        public string Host { get; set; }

    }
}
