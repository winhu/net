using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WinStudio.iTrip.Models
{
    public class NativePlace : TripBaseEntity
    {
        public NativePlace() { Children = new List<MongoDBRef>(); }

        public string Code { get; set; }

        public string Name { get; set; }

        public List<MongoDBRef> Children { get; set; }
    }

    public class Nationality : TripBaseEntity
    {
        public Nationality() { NativePlaces = new List<MongoDBRef>(); }

        public string Name { get; set; }

        public string Code { get; set; }

        public List<MongoDBRef> NativePlaces { get; set; }
    }
}
