using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;
using WinStudio.iTrip.Models;

namespace WinStuido.iTrip.ProfileBusiServ
{
    [Export(typeof(IProfileBusiness))]
    public class ProfileBusiServ : iTripBusiServ, IProfileBusiness
    {
        public Profile GetProfile(string account)
        {
            return MongoEntity.Get<Profile>(p => p.Account == account);
        }


        public List<Profile> GetFriends(string name)
        {
            return MongoEntity.Select<Profile>(p => p.Name.Contains(name)).ToList();
        }
    }
}
