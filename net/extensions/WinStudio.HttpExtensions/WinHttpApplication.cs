using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace WinStudio.HttpExtensions
{
    public class WinHttpApplication : HttpApplication
    {
        public Cache WinCache { get; set; }

        public override void Init()
        {
            base.Init();
            WinCache = new Cache();
        }

        public void CreateDependency(Object sender, EventArgs e)
        {
            // Create a DateTime object.
            DateTime dt = DateTime.Now.AddSeconds(10);

            // Create a cache entry.
            WinCache["key1"] = "Value 1";
            CacheDependency dependency = new CacheDependency(null, dt);

            WinCache.Insert("key2", "Value 2", dependency);
        }
    }
}
