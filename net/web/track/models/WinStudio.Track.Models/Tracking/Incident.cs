using System;
using System.Collections.Generic;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models.Tracking
{
    public class Incident : TrackModel
    {
        public Incident()
        {
            Type = IncidentType.Publish.ToString();
            Category = FocusType.Unkonwn.ToString();
        }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public int TrackerID { get; set; }

        public virtual Customer Tracker { get; set; }

        public int? ParentID { get; set; }

        public virtual Incident Parent { get; set; }

        public virtual List<Incident> Children { get; set; }
    }
}
