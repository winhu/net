using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Passport.BusiService.Intefaces
{
    public interface IUserSignHisTrace
    {
        void TraceHis(string account, string ip);
    }
    public interface IModuleSignHisTrace
    {
        void TraceHis(string module);
    }
}
