using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WinStudio.iTrip.Models
{
    public class Customer : TripBaseEntity
    {
        public Customer() { Friends = new List<MongoDBRef>(); }

        public string Account { get; set; }

        public string Name { get; set; }

        public List<MongoDBRef> Friends { get; set; }

    }
}
