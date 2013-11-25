using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.iTrip.ICommonBusiness;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.CommonBusiServ
{
    [Export(typeof(INationalityBusiness))]
    public class NationalityBusiServ : INationalityBusiness
    {
        public Nationality Get(string code)
        {
            return MongoEntity.Get<Nationality>(n => n.Code == code);
        }

        public List<Models.Nationality> GetList()
        {
            return MongoEntity.GetAll<Nationality>();
        }


        public List<NativePlace> GetNativePlaceList(string code)
        {
            var nation = Get(code);
            if (nation == null) return new List<NativePlace>();
            return nation.NativePlaces.RefPick<NativePlace>();
        }
    }
}
