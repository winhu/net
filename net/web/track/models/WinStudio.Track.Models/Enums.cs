using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Track.Models
{
    //public enum ContactType
    //{
    //    Email,
    //    QQ,
    //    Phone,
    //    Bolg
    //}

    public enum FocusType
    {
        [Description("未知")]
        Unkonwn = 0,
        [Description("娱乐")]
        Entertainment = 1,
        [Description("生活")]
        Life = 2,
        [Description("社会")]
        Society = 3,
        [Description("网络")]
        Net = 4
    }

    public enum IncidentType
    {
        [Description("未知")]
        Unkonwn = 0,
        [Description("发起")]
        Publish = 1,
        [Description("回复")]
        Reply = 2
    }
}
