using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CharacterSheet
{
    public class SQLManager
    {
        SqlConnection conn;

        public SQLManager(string userName, string password)
        {
            conn = new SqlConnection($"Data Source=dndserverpilo.database.windows.net;Initial Catalog=DnDDatabase;User ID={userName};Password={password};" +
                "Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
               conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    try
                    {
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return dt;
        }
    }
}
