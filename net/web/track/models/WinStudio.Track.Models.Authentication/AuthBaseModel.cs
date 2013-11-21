using System;
using System.ComponentModel.DataAnnotations;
using WinStudio.MongoDB.Models;

namespace WinStudio.Track.Models.Authentication
{
    public class AuthBaseModel : MongoModel
    {
        public AuthBaseModel() : base("Tracker") { }

    }
}
