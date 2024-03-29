﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Track.Models.Core
{
    public class LookupBase : TrackBaseModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Code, Name);
        }
    }
}
