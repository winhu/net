using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Models
{
    public class SysConfig
    {
        public bool UseWebConfig { get; set; }

        public string WebConfigConnectionName { get; set; }

        public string DbServer { get; set; }

        public string DbName { get; set; }

        public string DbUser { get; set; }

        public string DbPwd { get; set; }

        private string ConnectionString
        {
            get
            {
                if (UseWebConfig) return string.Empty;
                return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};Trusted_Connection=False;Persist Security Info=True", DbServer, DbName, DbUser, DbPwd);
            }
        }
        public string NameOrConnectionString
        {
            get
            {
                return UseWebConfig ? WebConfigConnectionName : ConnectionString;
            }
        }
    }
}
