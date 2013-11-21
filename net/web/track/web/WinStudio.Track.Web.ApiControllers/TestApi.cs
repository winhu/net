using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Track.Framework.Core.Abstract;

namespace WinStudio.Track.Web.ApiControllers
{
    public class TestApiController : AbstractTrackApiController
    {
        public IEnumerable<string> Get()
        {
            var lst = new List<String>() { "a", "b", "c" };
            return lst;
        }
    }
}
