using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Models.Base;
using WinStudio.Track.Models.Tracking;
using MongoDB.Bson;
using MongoDB;

namespace WinStudio.Track.Models.Tracking
{
    public class Customer : TrackModel
    {
        public Customer() { Incidents = new List<Incident>(); }

        public string Account { get; set; }


        public List<Incident> Incidents { get; set; }
    }
}

