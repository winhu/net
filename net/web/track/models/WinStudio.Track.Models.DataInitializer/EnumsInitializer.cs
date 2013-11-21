using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.TestDataFramework.EF;
using WinStudio.Track.Framework.Models;
using WinStudio.Track.Models.Lookups;

namespace WinStudio.Track.Models.DataInitializer
{
    public class EnumsInitializer : ITestDataInitializer<TrackDbContext>
    {
        public string Code
        {
            get { return "Enums"; }
        }

        public void Initialize(TrackDbContext context)
        {
            LookupBase.ConvertFromEnum<LookupFocusType, FocusType>().ForEach(l => context.Lookups.Add(l));
            LookupBase.ConvertFromEnum<LookupIncidentType, IncidentType>().ForEach(l => context.Lookups.Add(l));
        }

        public int Save(TrackDbContext context)
        {
            return context.SaveChanges();
        }
        public int Order
        {
            get { return 0; }
        }
    }
}
