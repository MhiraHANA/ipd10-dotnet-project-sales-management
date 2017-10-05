using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class Database
    {
        private string CONN_STRING = @"Server=tcp:h2m.database.windows.net,1433;Initial Catalog=SMSDB;Persist Security Info=False;User ID=sqladmin;Password=Sms@project;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection();
            conn.ConnectionString = CONN_STRING;
            conn.Open();
        }
    }

}
