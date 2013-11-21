using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Passport.Models
{
    public class PassportDbContext : WinDbContext
    {
        public PassportDbContext()
            : base(NameOrConnectionString)
        {
        }

        public PassportDbContext(bool useconfig, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            UserConfig = useconfig;
            NameOrConnectionString = nameOrConnectionString;
        }

        internal static bool UserConfig = true;
        internal static string NameOrConnectionString = "PassportDbContext";

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<SignerKey> SingerKeies { get; set; }
        public DbSet<OpenID> OpenIDs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserSignHistory> HisUserSign { get; set; }
        public DbSet<ModuleSignHistory> HisModuleSign { get; set; }
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
