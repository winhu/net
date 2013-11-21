using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.EnumExtensions
{
    public class EnumHelper
    {
        /// <summary>
        /// 得到类型成员的注释
        /// </summary>
        /// <param name="aType">类型定义</param>
        /// <param name="aName">成员名</param>
        /// <returns>注释,如果无注释则返回成员名</returns>
        public static string GetDescription(Type aType, string aName)
        {
            if (!aType.IsEnum) return string.Empty;
            if (string.IsNullOrEmpty(aName)) return string.Empty;
            MemberInfo[] minfos = aType.GetMember(aName);
            foreach (MemberInfo info in minfos)
            {
                foreach (Attribute attr in info.GetCustomAttributes(false))
                {
                    if (attr is DescriptionAttribute)
                        return ((DescriptionAttribute)attr).Description;
                }
            }
            return aName;
        }
    }
}
