using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Passport.Models;
using WinStudio.TestDataFramework.EF;

namespace WinStudio.Passport.TestDataInitializer
{
    public class ModuleInitializer : ITestDataInitializer<PassportDbContext>
    {
        public string Code
        {
            get { return this.GetType().FullName; }
        }

        public void Initialize(PassportDbContext context)
        {

            new List<Module>{ 
                new Module{  Name="身份认证模块", Code="Registration", Host="http://localhost:9000", IP="127.0.0.1", Status=true}
            }.ForEach(m => context.Modules.Add(m));
        }

        public int Order
        {
            get { return 0; }
        }

        public int Save(PassportDbContext context)
        {
            return context.SaveChanges();
        }
    }
}
