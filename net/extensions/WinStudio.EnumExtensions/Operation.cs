using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class Operation
    {
        /// <summary>
        /// get description attribute
        /// </summary>
        /// <param name="e">enum</param>
        /// <returns></returns>
        public static string GetDescription(this Enum e)
        {
            string name = e.ToString();
            MemberInfo[] members = e.GetType().GetMember(name);
            foreach (MemberInfo member in members)
            {
                if (member.Name == name)
                {
                    DescriptionAttribute attr = member.GetCustomAttribute<DescriptionAttribute>(true);
                    if (attr == null) return name;
                    return attr.Description;
                }
            }
            return string.Empty;
        }
        public static string GetDescription<TEnum>(this TEnum e)
            where TEnum : struct
        {
            DescriptionAttribute attr = e.GetAttribute<TEnum, DescriptionAttribute>();
            if (attr == null) return string.Empty;
            return attr.Description;
        }

        public static TAttribute GetAttribute<TEnum, TAttribute>(this TEnum e)
            where TEnum : struct
            where TAttribute : Attribute
        {
            if (!typeof(TEnum).IsEnum || !Enum.IsDefined(typeof(TEnum), e))
                return default(TAttribute);
            MemberInfo[] mi = typeof(TEnum).GetMember(e.ToString());
            if (mi.Length == 0) return default(TAttribute);
            return mi[0].GetCustomAttribute<TAttribute>(true);
        }

        public static T ToEnum<T>(this int val) where T : struct
        {
            if (!Enum.IsDefined(typeof(T), val)) return default(T);

            return val.ToString().ToEnum<T>();
        }

        public static T ToEnum<T>(this string val) where T : struct
        {
            if (string.IsNullOrEmpty(val)) throw new Exception("can not convert null or empty string");
            EnumConverter convert = new EnumConverter(typeof(T));
            if (convert == null)
                throw new ApplicationException("can not find EnumConverter");

            return (T)convert.ConvertFromString(val);
        }

        /// <summary>
        /// convert enum to array
        /// string[]{Description,Name,Value}
        /// </summary>
        /// <param name="e">enum</param>
        /// <returns></returns>
        public static string[] ToArray(this Enum e)
        {
            string[] args = new string[3];
            args[0] = e.GetDescription();
            args[1] = e.ToString();
            args[2] = e.GetHashCode().ToString();
            return args;
        }
    }
}
