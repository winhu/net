using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Permission.Models
{
    [MetadataType(typeof(AdministratorMetadata))]
    public partial class Administrator : BaseModel
    {
        public string Account { get; set; }

        public string Name { get; set; }

        public string Pwd { get; set; }

        public DateTime LastLoginTime { get { return lastLoginTime; } set { lastLoginTime = value; } }
        private DateTime lastLoginTime = DateTime.Now;

    }
}
