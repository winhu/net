using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Models;
using WinStudio.Track.Models.Authentication;

namespace WinStudio.Track.Models
{
    public class AuthenticationDbContext : BaseDbContext
    {
        public AuthenticationDbContext()
            : base(NameOrConnectionString)
        {
        }

        public AuthenticationDbContext(bool useconfig, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            UserConfig = useconfig;
            NameOrConnectionString = nameOrConnectionString;
        }

        internal static bool UserConfig = true;
        internal static string NameOrConnectionString = "AuthenticationDbContext";

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<SignerKey> SingerKeies { get; set; }
        public DbSet<OpenID> OpenIDs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<LookupBase> Lookups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TPH
            modelBuilder.Entity<LookupBase>().HasKey(x => x.ID);
            modelBuilder.Entity<LookupBase>().Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<LookupBase>().ToTable("Lookups");

            modelBuilder.Entity<SignerKey>().HasKey(x => x.Account);
            modelBuilder.Entity<SignerKey>().Property(x => x.Account)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
