using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.ICommonBusiness
{
    public interface INationalityBusiness
    {
        Nationality Get(string code);
        List<Nationality> GetList();
        List<NativePlace> GetNativePlaceList(string code);
    }
}
