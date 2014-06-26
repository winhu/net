using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.iTrip.Models
{
    public class Initializer
    {
        public void Run() { }

        public List<Nationality> InitializeNationality()
        {
            return new List<Nationality>()
            {
                new Nationality(){ Code="China", Name="中国"},
                new Nationality(){ Code="American", Name="美国"}
            };
        }
        public List<NativePlace> InitializeNativePlace(Nationality nationality)
        {
            var list = new List<NativePlace>()
            {
                new NativePlace(){ Code="BeiJing", Name="北京"},
                new NativePlace(){ Code="TianJin", Name="天津"},
                new NativePlace(){ Code="ShangHai", Name="上海"},
                new NativePlace(){ Code="Chongqing", Name="重庆"},
                new NativePlace(){ Code="Guangzhou", Name="广州"}
            };
            list.ForEach(np => nationality.NativePlaces.Add(np.ToDBRef()));
            return list;
        }
    }
}
