using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class DBConnection
    {
        private static string _connectionString =
            @"Data Source=MOHAMEDHP\SQLEXPRESS01;Initial Catalog=SmallBussinessHubDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            return conn;
        }
    }
}
