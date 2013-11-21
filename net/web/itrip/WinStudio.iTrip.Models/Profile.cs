using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.iTrip.Models
{
    public class Profile : TripBaseEntity
    {
        public string Account { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
