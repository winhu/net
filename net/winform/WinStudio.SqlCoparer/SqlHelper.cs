using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WinStudio.SqlComparer
{
    public class SqlHelper
    {
        private string server, username, password;
        public SqlHelper(string server, string database, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", server, database, username, password);

        }
        public SqlHelper(SqlConnection conn)
        {
            this.sqlConn = conn;
        }
        System.Data.SqlClient.SqlConnection sqlConn;
        System.Data.SqlClient.SqlCommand sqlCmd;

        public bool Open()
        {
            try
            {
                sqlConn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
        }

        public void Close()
        {
            try
            {
                if (sqlConn.State != System.Data.ConnectionState.Closed)
                    sqlConn.Close();
            }
            catch
            {
            }
        }

        public DataSet GetDataSet(string sql)
        {
            sqlCmd = new System.Data.SqlClient.SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = sql;
            return FillDataSet(sqlCmd);
        }

        public DataSet FillDataSet(SqlCommand cmd)
        {
            using (SqlDataAdapter dpr = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                dpr.Fill(ds);
                return ds;
            }
        }
    }
}
