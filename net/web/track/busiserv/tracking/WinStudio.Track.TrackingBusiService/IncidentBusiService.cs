using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Web.Common.Auto;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core;
using WinStudio.Track.Framework.Core.Abstract;
using WinStudio.Track.ITrackingBusiService;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.TrackingBusiService
{
    [AutoNinject]
    public class IncidentBusiService : AbstractBusiService<Incident>, IIncidentBusiService
    {
        public ComRet GetIncidents(string key, int pageIndex, int pageSize)
        {
            return Result(GetAll(i => i.Title.Contains(key) && i.Deleted == false).OrderByDescending(i => i.CreatedTime).Take(pageSize).Skip(pageIndex * pageSize).ToList());
        }

        public ComRet GetIncident(int id)
        {
            return Result(GetById(id));
        }

        public ComRet UpdateIncident(Incident incident)
        {
            Update(incident);
            return Result();
        }

        public ComRet DeleteIncident(int id)
        {
            Delete(id);
            return Result();
        }
    }
}
