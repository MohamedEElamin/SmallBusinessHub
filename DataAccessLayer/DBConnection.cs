using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataObjectLayer;

namespace DataAccessLayerInterfaces
{
    public static class DBConnection
    {

        // Server name: smallbussinesshubdb

        // Server admin login smallbussinesshubdbadmin

        // pASSWORD : Talia2022@

        //  @"Data Source=MOHAMEDHP\SQLEXPRESS01;Initial Catalog=SmallBussinessHubDB;Integrated Security=True";


        // Server=tcp:smallbussinesshubdb.database.windows.net,1433;Initial Catalog=SmallBussinessHubDB;
        // Persist Security Info=False;User ID=smallbussinesshubdbadmin;Password={Talia2022@};
        // MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        //Data Source=smallbussinesshubdb.database.windows.net;Initial Catalog=SmallBussinessHubDB;Persist Security Info=True;User ID=smallbussinesshubdbadmin;Password=***********;Trust Server Certificate=True
        private static string _connectionString =

        @"Data Source = smallbussinesshubdb.database.windows.net;
        Initial Catalog = SmallBussinessHubDB; Persist Security Info=True;
            User ID = smallbussinesshubdbadmin; Password=Talia2022@;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            return conn;
        }
    }
}
