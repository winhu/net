
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Models.ModelConfiguration
{

    class IncidentConfiguration : EntityTypeConfiguration<Incident>
    {
        internal IncidentConfiguration()
        {
            this.HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentID);
        }
    }
}
