using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using WinStudio.Track.Models.Base;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Models
{
    public class TimePoint : TrackModel
    {
        public TimePoint()
        { }
        public TimePoint(string lineID)
        {
            LineID = lineID;
        }

        public LookupBase PointType { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string LineID { get; set; }

        public Location Location { get; set; }
    }
}
