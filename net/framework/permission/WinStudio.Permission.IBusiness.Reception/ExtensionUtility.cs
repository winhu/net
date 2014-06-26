﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Permission.IBusiness.Reception
{
    public static class ExtensionUtility
    {
        public static string GetSecurityKey(this HttpContextBase context)
        {
            return context.GetToken(WinWebGlobalManager.Config.WinTokenName);
        }

    }
}
