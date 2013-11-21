using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Track.Models;

namespace WinStudio.Track.IBusiService
{
    public interface ITimeLineBusiService
    {
        ComRet GetTimeLine(string id);
        ComRet SaveTimeLine(TimeLine line, string account);
        ComRet GetTimeLines(string key, int pageIndex, int pageSize);

        ComRet AddTimePoint(TimePoint point);
    }
}
