using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.Passport.BusiService.Abstract;
using WinStudio.Passport.BusiService.Intefaces;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.BusiService.Impl
{
    [Export(typeof(IModuleSignHisTrace))]
    public class HisModuleSignBusiService : AbstractPassportBusiService<ModuleSignHistory>, IModuleSignHisTrace
    {
        public void TraceHis(string module)
        {
            if (string.IsNullOrEmpty(module)) return;
            DateTime stamp = DateTime.Parse(string.Format("{0}-{1}-1", DateTime.Now.Year, DateTime.Now.Month));
            ModuleSignHistory his = MongoEntity.Get<ModuleSignHistory>(m => m.Code == module && m.CreatedTime > stamp);
            if (his != null)
            {
                his.Counter++;
                his.Save();
            }
            else
            {
                ModuleSignHistory h = new ModuleSignHistory() { Code = module, LastDate = DateTime.Now };
                h.Save();
            }

        }
    }
}
