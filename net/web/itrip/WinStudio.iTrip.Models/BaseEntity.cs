using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;

namespace WinStudio.iTrip.Models
{
    public class TripBaseEntity : Entity
    {
        public virtual bool IsValid() { return true; }

        public string GetMsg() { return string.Empty; }
    }
}
