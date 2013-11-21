using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{

    class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        internal RoleConfiguration()
        {
            this.HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentID);
        }
    }
    class FunctionConfiguration : EntityTypeConfiguration<Function>
    {
        internal FunctionConfiguration()
        {
            this.HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentID);
        }
    }
    class RFuncConfiguration : EntityTypeConfiguration<RFunc>
    {
        internal RFuncConfiguration()
        {
            this.HasOptional(c => c.Parent)
                .WithMany().WillCascadeOnDelete(true);
        }
    }
}
