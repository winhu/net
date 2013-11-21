using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WinStudio.SqlComparer
{
    public class DbConnector
    {
        private string server, username, password;
        SqlHelper sql;
        public DbConnector(string server, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            sql = new SqlHelper(server, "master", username, password);

        }

        public bool Open()
        { return sql.Open(); }

        public DataSet GetDatabases()
        {
            return sql.GetDataSet("select name from [sys].[databases]");
        }

        public DataSet GetDataTables(string database)
        {
            return sql.GetDataSet(string.Format("use {0} select name as cname,object_id from [sys].[tables] order by name", database));
        }

        public DataSet GetDataTableColumns(string database, string table)
        {
            return sql.GetDataSet(string.Format("use {0} select * from [sys].[all_columns] where name='{1}' order by name", database, table));
        }

        public DataSet GetDataTableColumsInfo(string database, string table)
        {
            return sql.GetDataSet(string.Format("use {0} select ac.name as cname,ac.system_type_id as stid, ac.max_length as ml, st.[length] as l,st.name as tname,ac.is_nullable as isnl,ac.is_identity as isid,ac.object_id from sys.all_columns ac inner join sys.tables t on ac.object_id=t.object_id inner join [master].sys.systypes st on ac.system_type_id=st.xusertype where t.name='{1}' order by ac.name", database, table));
        }

    }

}
