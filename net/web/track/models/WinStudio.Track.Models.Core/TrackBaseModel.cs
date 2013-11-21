using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WinStudio.Track.Models.Core
{
    public interface ITrackModel
    {
        ObjectId ID { get; set; }
        bool Deleted { get; set; }
        DateTime CreateTime { get; set; }

        string DatabaseName { get; }
        string CollectionName { get; }
    }
    public class TrackBaseModel : ITrackModel
    {
        public TrackBaseModel()
        {
            CreateTime = DateTime.Now;
        }

        public ObjectId ID { get; set; }

        public bool Deleted { get; set; }

        [BsonElementAttribute("createtime")]
        public DateTime CreateTime { get; set; }


        public string DatabaseName
        {
            get { return "TrackDB"; }
        }

        public string CollectionName
        {
            get { return this.GetType().Name; }
        }
    }
}
