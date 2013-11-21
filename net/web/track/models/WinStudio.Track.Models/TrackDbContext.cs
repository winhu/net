using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Models
{
    public class TrackDbContext : BaseDbContext
    {
        public TrackDbContext()
            : base(NameOrConnectionString)
        {
        }

        public TrackDbContext(bool useconfig, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            UserConfig = useconfig;
            NameOrConnectionString = nameOrConnectionString;
        }

        internal static bool UserConfig = true;
        internal static string NameOrConnectionString = "TrackDbContext";

        public DbSet<Customer> Customers { get; set; }
        //public DbSet<Profile> Profiles { get; set; }
        //public DbSet<SignerKey> SingerKeies { get; set; }
        //public DbSet<OpenID> OpenIDs { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
        public DbSet<LookupBase> Lookups { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Detail> Details { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TPH
            modelBuilder.Entity<LookupBase>().HasKey(x => x.ID);
            modelBuilder.Entity<LookupBase>().Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<LookupBase>().ToTable("Lookups");

            //modelBuilder.Entity<SignerKey>().HasKey(x => x.Account);
            //modelBuilder.Entity<SignerKey>().Property(x => x.Account)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Incident>().HasOptional(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentID);
        }
    }
}
