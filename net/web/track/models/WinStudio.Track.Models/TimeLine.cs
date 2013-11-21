using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WinStudio.Track.Models.Base;

namespace WinStudio.Track.Models
{
    public class TimeLine : TrackModelDBRef
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public bool NeedCertification { get; set; }

        public TimePoint GetPoint(int index)
        {
            Refs.ForEach(delegate(MongoDBRef r)
            {
                if (r.CollectionName == typeof(TimePoint).Name)
                {

                }
            });

            return null;
        }

        public List<TimePoint> Points
        {
            get
            {
                return GetRefInstances<TimePoint>();
            }
        }
    }
}
