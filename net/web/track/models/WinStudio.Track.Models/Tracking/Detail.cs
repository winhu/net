using System;
using System.Collections.Generic;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models.Tracking
{
    public class Detail : TrackModel
    {
        public int IncidentID { get; set; }
        public string Content { get; set; }

        public virtual List<Attachment> Attachments { get; set; }
    }
}
