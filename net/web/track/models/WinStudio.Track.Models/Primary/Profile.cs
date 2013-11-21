using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.MongoDB.Models;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models.Primary
{
    public class Profile : TrackModel
    {
        public string Account { get; set; }

        public string NickName { get; set; }

        public Gender Gender { get; set; }

        public Contact Contact { get; set; }

        public long Integral { get; set; }

        public string PassportNumber { get; set; }

        public bool IsActived { get; set; }

        public DateTime LastLoginTime { get; set; }
    }
}
