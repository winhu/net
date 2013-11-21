using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.IBusiness
{
    public interface IProfileBusiness
    {
        Profile GetProfile(string account);

        List<Profile> GetFriends(string name);
    }
}
