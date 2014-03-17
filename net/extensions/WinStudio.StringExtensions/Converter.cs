using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace System
{
    public static class Converter
    {
        /// <summary>
        /// convert string to int32
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="v">default value(0)</param>
        /// <returns></returns>
        public static int ToInt(this string s, int v = 0)
        {
            if (s.HasValue())
            {
                Int32.TryParse(s, out v);
            }
            return v;
        }

        /// <summary>
        /// convert string to DateTime
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="v">default value(DateTime.MinValue)</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime v, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (s.HasValue())
            {
                DateTime.TryParseExact(s, format, null, System.Globalization.DateTimeStyles.AssumeLocal, out v);
                //DateTime.TryParse(s, out v);
            }
            return v;
        }

        /// <summary>
        /// convert string to DateTime(default value is DateTime.MinValue)
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, string format = "yyyy-MM-dd HH:mm:ss")
        {
            DateTime v = DateTime.MinValue;
            return s.ToDateTime(v, format);
        }

        /// <summary>
        /// convert string to Boolean(default value is False)
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool ToBoolean(this string s, bool v = false)
        {
            if (!s.HasValue()) return false;
            Boolean.TryParse(s, out v);
            return v;
        }

        /// <summary>
        /// convert string to Float
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="v">default value(0)</param>
        /// <returns></returns>
        public static float ToFloat(this string s, float v = 0)
        {
            if (s.HasValue())
                float.TryParse(s, out v);
            return v;
        }
        /// <summary>
        /// convert string to Double
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="v">default value(0)</param>
        /// <returns></returns>
        public static double ToDouble(this string s, double v = 0)
        {
            if (s.HasValue())
                double.TryParse(s, out v);
            return v;
        }
        /// <summary>
        /// convert string to Decimal
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="v">default value(0M)</param>
        /// <returns></returns>
        public static Decimal ToDecimal(this string s, decimal v = 0)
        {
            if (s.HasValue())
                Decimal.TryParse(s, out v);
            return v;
        }

        /// <summary>
        /// convert string to Enum
        /// </summary>
        /// <typeparam name="TEnum">Enum</typeparam>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string s, TEnum v) where TEnum : struct
        {
            if (!s.HasValue()) return default(TEnum);
            EnumConverter convert = new EnumConverter(typeof(TEnum));
            if (convert == null)
                return v;

            return (TEnum)convert.ConvertFromString(s);
        }        /// <summary>
        /// convert string to Enum
        /// </summary>
        /// <typeparam name="TEnum">Enum</typeparam>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string s) where TEnum : struct
        {
            if (!s.HasValue()) return default(TEnum);
            EnumConverter convert = new EnumConverter(typeof(TEnum));
            if (convert == null)
                throw new Exception("Canot find EnumConverter");

            return (TEnum)convert.ConvertFromString(s);
        }

        /// <summary>
        /// convert string to int array
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="c">spliter char string, like ".,;"</param>
        /// <returns></returns>
        public static int[] ToIntArray(this string s, string c = ",")
        {
            if (s.HasValue())
            {
                string[] ds = s.Split(s.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                return ds.Select(d => d.ToInt(0)).ToArray();
            }
            return new int[] { };
        }


        /// <summary>
        /// convert xml string to object
        /// </summary>
        /// <typeparam name="TObject">objecct</typeparam>
        /// <param name="s">xml string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        public static TObject ToObject<TObject>(this string s, Encoding encoding) where TObject : class
        {
            if (!s.HasValue()) return default(TObject);
            byte[] byteArray = encoding.GetBytes(s);
            MemoryStream stream = new MemoryStream(byteArray);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(TObject));
            return xs.Deserialize(stream) as TObject;
        }

        public static string ToXml(this object target, Encoding encoding)
        {
            if (target != null)
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(target.GetType());
                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, encoding);
                x.Serialize(writer, target);
                byte[] EncodedData = stream.GetBuffer();
                return Encoding.UTF8.GetString(EncodedData, 0, (int)stream.Length);
            }
            else return string.Empty;
        }
        public static string ToXml(this object target)
        {
            return target.ToXml(Encoding.UTF8);
        }

        public static NameValueCollection ToNameValueCollection(this string s, string spliter = "=&")
        {
            NameValueCollection nvc = new NameValueCollection();
            if (!s.HasValue()) return nvc;
            if (s.HasValue() && spliter.Length == 2)
            {
                string[] arr = s.Split(spliter.ToArray());
                if (arr.Length % 2 == 0)
                {
                    for (int i = 0; i < arr.Length; )
                    {
                        nvc.Add(arr[i++], arr[i++]);
                    }
                }
            }
            return nvc;
        }

        public static byte[] ToUTF8Bytes(this string s)
        {
            if (!s.HasValue()) return new byte[0];
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
