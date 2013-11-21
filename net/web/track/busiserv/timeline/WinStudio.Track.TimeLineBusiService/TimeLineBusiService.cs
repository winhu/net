using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core;
using WinStudio.Track.IBusiService;
using WinStudio.Track.Models;

namespace WinStudio.Track.TimeLineBusiService
{
    [Export(typeof(ITimeLineBusiService))]
    public class TimeLineBusiService : AbstractTrackBusiService<TimeLine>, ITimeLineBusiService
    {
        public ComRet GetTimeLine(string id)
        {
            return Result(GetById(id));
        }
        public ComRet SaveTimeLine(TimeLine line, string account)
        {
            if (Exist(t => t.Name == line.Name && t.Id != line.Id)) { return Result("时间轴重名，请修改您的时间轴名称！"); }
            Save(line);
            return Result(true, "时间轴保存成功！");
        }

        public ComRet GetTimeLines(string key, int pageIndex = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(key))
            {
                var lst = GetPaged(t => t.Name != null, t => t.Id, pageIndex, pageSize);
                return Result(lst);
            }
            var ret = GetPaged(t => t.Name.Contains(key), t => t.Id, pageIndex, pageSize);
            return Result(ret);

            var query = GetAll(t => t.Name.Contains(key));
            if (query == null) return Result(false);
            return Result(GetAll(t => t.Name.Contains(key)).OrderByDescending(t => t.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


        public ComRet AddTimePoint(TimePoint point)
        {
            var line = GetById(point.LineID);
            if (line == null) return Result("时间轴不存在！");
            //line.Points.Add(point);
            line.AddRef(point);
            //line.Points.Add(point.ToDBRef());
            Save(line);
            return Result();
        }
    }
}
