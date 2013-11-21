using System;
using System.Reflection;
using log4net;

namespace System
{
    public static class PermissionLogger
    {
        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }
        public static ILog GetLogger()
        {
            string t = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            ILog log = LogManager.GetLogger(t);
            return log;
        }

        public static void LogError(this Exception e, string msg = "")
        {
            GetLogger().Error(msg, e);
        }
        public static void LogDebug(this Exception e, string msg = "")
        {
            GetLogger().Debug(msg, e);
        }
        public static void LogInfo(this Exception e, string msg = "")
        {
            GetLogger().Info(msg, e);
        }
        public static void LogWarn(this Exception e, string msg = "")
        {
            GetLogger().Warn(msg, e);
        }
        public static void LogFatal(this Exception e, string msg = "")
        {
            GetLogger().Fatal(msg, e);
        }

        public static void LogError(this string msg, Exception e = null)
        {
            GetLogger().Error(msg, e);
        }
        public static void LogDebug(this string msg, Exception e = null)
        {
            GetLogger().Debug(msg, e);
        }
        public static void LogInfo(this string msg, Exception e = null)
        {
            GetLogger().Info(msg, e);
        }
        public static void LogWarn(this string msg, Exception e = null)
        {
            GetLogger().Warn(msg, e);
        }
        public static void LogFatal(this string msg, Exception e = null)
        {
            GetLogger().Fatal(msg, e);
        }

        public static void LogError(this object o, Exception e = null)
        {
            GetLogger().Error(o, e);
        }
        public static void LogDebug(this object o, Exception e = null)
        {
            GetLogger().Debug(o, e);
        }
        public static void LogInfo(this object o, Exception e = null)
        {
            GetLogger().Info(o, e);
        }
        public static void LogWarn(this object o, Exception e = null)
        {
            GetLogger().Warn(o, e);
        }
        public static void LogFatal(this object o, Exception e = null)
        {
            GetLogger().Fatal(o, e);
        }

        //public void LogError(string msg, Exception e = null)
        //{
        //    GetLogger().Error(msg, e);
        //}
        //public void LogDebug(string msg, Exception e = null)
        //{
        //    GetLogger().Debug(msg, e);
        //}
        //public void LogInfo(string msg, Exception e = null)
        //{
        //    GetLogger().Debug(msg, e);
        //}
        //public void LogWarn(string msg, Exception e = null)
        //{
        //    GetLogger().Warn(msg, e);
        //}
        //public void LogFatal(string msg, Exception e = null)
        //{
        //    GetLogger().Fatal(msg, e);
        //}
    }
}
