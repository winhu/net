using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;

namespace WinStudio.Track.Models
{
    public static class Utilities
    {
        //public static Oid ToOid(this string s)
        //{
        //    if (string.IsNullOrEmpty(s))
        //        return null;
        //    return new Oid(s);
        //}

        public static ObjectId ToObjectId(this string s)
        {
            if (string.IsNullOrEmpty(s)) return ObjectId.Empty;
            ObjectId id;
            ObjectId.TryParse(s, out id);
            return id;
        }
    }
}
