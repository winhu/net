using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core.Abstract;
using WinStudio.Track.ITrackingBusiService;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Web.ApiControllers
{
    public class TrackApiController : AbstractTrackApiController
    {
        private IIncidentBusiService _bsIncident;

        [NeedSignIn]
        public IEnumerable<Incident> GetIncidents(string key = null, int pageIndex = 0, int pageSize = 30)
        {
            ComRet ret = _bsIncident.GetIncidents(key, pageIndex, pageSize);
            var lst = ret.Instance<List<Incident>>();
            return lst;
        }

        public IEnumerable<Incident> AllIncidents(string key = null, int pageIndex = 0, int pageSize = 30)
        {
            ComRet ret = _bsIncident.GetIncidents(key, pageIndex, pageSize);
            var lst = ret.Instance<List<Incident>>();
            return lst;
        }
        public Incident GetIncident(int id)
        {
            ComRet ret = _bsIncident.GetIncident(id);
            return ret.Instance<Incident>();
        }
        public ComRet UpdateIncident([FromBody]Incident endity)
        {
            ComRet ret = _bsIncident.UpdateIncident(endity);
            return ret;
        }
        public ComRet DeleteIncident(int id)
        {
            ComRet ret = _bsIncident.GetIncident(id);
            return ret;
        }
    }
}
