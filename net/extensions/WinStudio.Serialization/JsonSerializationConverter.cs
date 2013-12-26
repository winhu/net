using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.Serialization.Json
{
    public static class JsonSerializationConverter
    {
        public static To ConvertTo<To, From>(this From source)
            where To : class
            where From : class
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(From));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, source);
            string jsonstr = Encoding.UTF8.GetString(ms.ToArray());

            dcjs = new DataContractJsonSerializer(typeof(To));
            ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonstr));
            return dcjs.ReadObject(ms) as To;
        }

        public static string ToJsonStr(this object source)
        {
            if (source == null) return string.Empty;
            Type type = source.GetType();
            if (type.IsAbstract || type.IsInterface || !type.IsPublic) return string.Empty;

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(type);
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, source);
            string jsonstr = Encoding.UTF8.GetString(ms.ToArray());
            return jsonstr;
        }
        public static T FromJsonStr<T>(this string str) where T : class
        {
            if (string.IsNullOrEmpty(str)) return default(T);
            Type type = typeof(T);
            if (type.IsAbstract || type.IsInterface || !type.IsPublic) return default(T);

            var dcjs = new DataContractJsonSerializer(type);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return dcjs.ReadObject(ms) as T;
        }
    }

}
