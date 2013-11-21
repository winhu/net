using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.EFModels;
using WinStudio.Framework.Web.Repository;

namespace WinStudio.Framework.Web.Services
{
    public interface IWinBusiService
    {
        WinDbContext GetContext();
        void SetContext(WinDbContext factory);
    }
}
