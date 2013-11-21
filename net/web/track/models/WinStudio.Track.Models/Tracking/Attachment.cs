using System;
using System.Collections.Generic;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models.Tracking
{
    public class Attachment : TrackModel
    {
        public int ContentID { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Size { get; set; }
    }
}
