using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;
using WinStudio.TestDataFramework.EF;

namespace WinStudio.Permission.TestDataInitializer
{
    public class RFuncInitializer : ITestDataInitializer<PermissionDbContext>
    {
        public string Code
        {
            get { return "RFunc"; }
        }

        public void Initialize(PermissionDbContext context)
        {
            //删除关联
            //context.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[RFuncs] DROP CONSTRAINT [FK_dbo.RFuncs_dbo.RFuncs_ParentID]");
            //context.RFuncs.Add(new RFunc() { Name = "功能根节点"});
        }

        public int Order
        {
            get { return 3; }
        }

        public int Save(PermissionDbContext context)
        {
            return context.Commit();
        }
    }
}
