using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Web.Common.Auto;
using WinStudio.ComResult;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.ITrackingBusiService
{
    public interface IIncidentBusiService
    {
        ComRet GetIncidents(string key, int pageIndex, int pageSize);
        ComRet GetIncident(int id);
        ComRet UpdateIncident(Incident incident);
        ComRet DeleteIncident(int id);
    }
}
