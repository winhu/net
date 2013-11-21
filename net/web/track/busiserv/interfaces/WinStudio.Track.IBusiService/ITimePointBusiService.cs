using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using WinStudio.ComResult;
using WinStudio.Track.Models;

namespace WinStudio.Track.IBusiService
{
    public interface ITimePointBusiService
    {
        ComRet GetTimePoint(string id);
        ComRet GetTimePoint(string lineID, string id);
        ComRet SaveTimePoint(TimePoint point, string account);
        ComRet GetTimePoints(string key, int pageIndex, int pageSize);
    }
}
