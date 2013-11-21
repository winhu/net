using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WinStudio.MongoDB.IRepository;
using WinStudio.MongoDB.Repository;
using WinStudio.MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace WinStudio.Track.Models.Base
{
    public abstract class TrackModel : MongoModel
    {
        public TrackModel() : this("Tracker") { }
        public TrackModel(string dbName) : base(dbName) { }
    }

    public abstract class TrackModelDBRef : MongoModelDBRef
    {
        public TrackModelDBRef() : this("Tracker") { }
        public TrackModelDBRef(string dbName) : base(dbName) { }
    }
}
