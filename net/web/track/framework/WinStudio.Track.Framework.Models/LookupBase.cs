using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.EnumExtensions;

namespace WinStudio.Track.Framework.Models
{
    public class LookupBase : BaseModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(128)]
        public string Value { get; set; }

        private static object locker = new object();
        /// <summary>
        /// 将枚举类型转换成字典类型：
        /// Lookup.Name=Enum.Description，
        /// Lookup.Code=Enum.Name，
        /// Lookup.Value=Enum.Value
        /// </summary>
        /// <typeparam name="TLookup">字典类型</typeparam>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <returns></returns>
        public static List<TLookup> ConvertFromEnum<TLookup, TEnum>()
            where TLookup : LookupBase
            where TEnum : struct
        {
            if (typeof(TEnum).IsEnum)
            {
                lock (locker)
                {
                    List<TLookup> lst = new List<TLookup>();
                    foreach (string name in Enum.GetNames(typeof(TEnum)))
                    {
                        TEnum tenum = name.ToEnum<TEnum>();
                        TLookup lookup = Activator.CreateInstance<TLookup>();
                        lookup.Name = tenum.GetDescription<TEnum>();
                        lookup.Code = name;
                        lookup.Value = ((int)tenum.GetType().GetField(name).GetValue(tenum)).ToString();
                        lst.Add(lookup);
                    }
                    return lst;
                }
            }
            return null;
        }

        public static TLookup CreateFromEnum<TLookup>(Enum aEnum)
            where TLookup : LookupBase
        {
            TLookup lookup = Activator.CreateInstance<TLookup>();
            lookup.Name = aEnum.GetDescription();
            lookup.Code = aEnum.ToString();
            lookup.Value = aEnum.GetHashCode().ToString();
            return lookup;
        }

        public static TLookup CreateFromEnum<TLookup>(Enum aEnum, string aValue)
            where TLookup : LookupBase
        {
            TLookup lookup = Activator.CreateInstance<TLookup>();
            lookup.Name = aEnum.GetDescription();
            lookup.Code = aEnum.ToString();
            lookup.Value = aValue;
            return lookup;
        }
    }
}
