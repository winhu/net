using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WinStudio.iTrip.Models
{
    public class ShortMessage : TripBaseEntity
    {
        public ShortMessage() { Receiver = new List<MongoDBRef>(); }

        public string Sender { get; set; }

        public string Content { get; set; }

        public List<MongoDBRef> Receiver { get; set; }
    }
}
