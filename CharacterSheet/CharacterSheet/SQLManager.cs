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

        public DataTable SelectQuery(string query)
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

        public DataTable ParameterizedSelectQuery(string query, List<QueryParameter> parsList)
        {
            DataTable dt = new DataTable();
            SqlParameter par;
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                //TODO: Switch per ogni tipo?
                foreach(var p in parsList)
                {
                    par = new SqlParameter();
                    par.Value = p.Value;
                    par.ParameterName = p.Name;
                    command.Parameters.Add(par);
                }

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

    public class QueryParameter
    {
        public string Name;
        public object Value;
        public Type ValueType;

        public QueryParameter(string name, object value, Type valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }
    }
}
