using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;

namespace WinStudio.Permission.Models
{
    public class PermissionDbContext : WinDbContext
    {
        public PermissionDbContext()
            : base(NameOrConnectionString)
        {
        }

        public PermissionDbContext(bool useconfig, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            UserConfig = useconfig;
            NameOrConnectionString = nameOrConnectionString;
        }

        internal static bool UserConfig = true;
        internal static string NameOrConnectionString = "PermissionDbContext";

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SysModule> SysModules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<RFunc> RFuncs { get; set; }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<History> Histories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new RFuncConfiguration());
            //modelBuilder.Configurations.Add(new FunctionConfiguration());
            //modelBuilder.Configurations.Add(new RoleConfiguration());
            //modelBuilder.Entity<RFunc>().HasMany(f => f.Children).WithRequired().WillCascadeOnDelete(true);//.HasOptional(f => f.Parent).WithMany(f => f.Children).WillCascadeOnDelete(true);
            //modelBuilder.Entity<Role>().HasMany(r => r.Functions).WithMany(f => f.Roles).Map(i =>
            //{

            //    i.ToTable("RoleFunctions");
            //    i.MapLeftKey("RoleID");
            //    i.MapRightKey("FunctionID");
            //});
            //modelBuilder.Entity<Role>().HasMany(r => r.Functions).WithOptional().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Function>().HasMany(r => r.Roles).WithOptional().WillCascadeOnDelete(false);

        }
    }
}
