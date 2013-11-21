using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
    public static class Session
    {
        public static void SaveToSession(this HttpContextBase context, string key, object obj)
        {
            context.Session[key] = obj;
        }

        public static object GetFromSession(this HttpContextBase context, string key)
        {
            if (context.Session[key] == null) return null;
            return context.Session[key];
        }
        public static void RemoveFromSession(this HttpContextBase context, string key)
        {
            context.Session.Remove(key);
        }
        public static T GetFromSession<T>(this HttpContextBase context, string key) where T : class
        {
            if (context.Session[key] == null) return default(T);
            return context.Session[key] as T;
        }
        public static T GetFromSession<T>(this HttpContextBase context) where T : class
        {
            foreach (string key in context.Session.Keys)
            {
                var v = context.Session[key];
                if (v.GetType() == typeof(T))
                    return v as T;
            }
            return default(T);
        }

        public static bool HasSessionKey(this HttpContextBase context, string key)
        {
            if (context.Session[key] == null)
            {
                context.Session.Remove(key);
                return false;
            } return true;
        }

    }

}
