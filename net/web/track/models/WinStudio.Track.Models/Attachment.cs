using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models
{
    public class Attachment : TrackModel
    {
        public Attachment()
        {

        }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int Size { get; set; }
    }
}
