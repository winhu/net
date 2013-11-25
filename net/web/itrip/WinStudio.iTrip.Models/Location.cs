using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.iTrip.Models
{
    public class Location : TripBaseEntity
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public DateTime Date { get; set; }
    }
}
