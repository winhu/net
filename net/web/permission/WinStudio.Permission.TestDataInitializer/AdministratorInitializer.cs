using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;
using WinStudio.TestDataFramework.EF;

namespace WinStudio.Permission.TestDataInitializer
{
    public class AdministratorInitializer : ITestDataInitializer<PermissionDbContext>
    {
        public string Code
        {
            get { return this.GetType().FullName; }
        }

        public int Order
        {
            get { return 0; }
        }

        public void Initialize(PermissionDbContext context)
        {

            new List<Administrator>() {
                new Administrator{ Name="系统管理员", Account="administrator",Pwd="admin888".ToMD5() }
            }.ForEach(a => context.Administrators.Add(a));
        }

        public int Save(PermissionDbContext context)
        {
            return context.SaveChanges();
        }
    }
}
