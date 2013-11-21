using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core;
using WinStudio.Track.IBusiService;
using WinStudio.Track.Models;

namespace WinStudio.Track.TimeLineBusiService
{
    [Export(typeof(ITimePointBusiService))]
    public class TimePointBusiService : AbstractTrackBusiService<TimePoint>, ITimePointBusiService
    {
        public ComRet GetTimePoint(string id)
        {
            return Result(GetById(id));
        }
        public ComRet GetTimePoint(string lineID, string id)
        {
            var point = Get(prop => prop.LineID == lineID && prop.Id == id);
            if (point == null)
                point = new TimePoint(lineID);
            return Result(point);
        }

        public ComRet SaveTimePoint(TimePoint point, string account)
        {
            //if (Exist(t => t.Name == line.Name && t.ID != line.ID)) { return Result("时间轴重名，请修改您的时间轴名称！"); }
            Save(point);
            var ibsTimeLine = GetService<ITimeLineBusiService>();
            ibsTimeLine.AddTimePoint(point);
            return Result(true, "时间点保存成功！");
        }

        public ComRet GetTimePoints(string key, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(key))
            {
                var lst = GetPaged(t => t.Summary != null, t => t.Id, pageIndex, pageSize);
                return Result(lst);
            }
            var ret = GetPaged(t => t.Summary.Contains(key), t => t.Id, pageIndex, pageSize);
            return Result(ret);

            var query = GetAll(t => t.Summary.Contains(key));
            if (query == null) return Result(false);
            return Result(GetAll(t => t.Summary.Contains(key)).OrderByDescending(t => t.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
    }
}
