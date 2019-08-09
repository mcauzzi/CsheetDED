using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CharacterSheet
{
    public class SQLManager
    {
        readonly SqlConnection conn;

        public SQLManager(string userName, string password)
        {
            conn = new SqlConnection($"Data Source=dndserverpilo.database.windows.net;Initial Catalog=DnDDatabase;User ID={userName};Password={password};" +
                "Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();
        }

        public DataTable SelectQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable ParameterizedSelectQuery(string query, List<QueryParameter> parsList)
        {
            DataTable dt = new DataTable();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                //TODO: Switch per ogni tipo?
                foreach(var p in parsList)
                {
                    var par = new SqlParameter {Value = p.Value, ParameterName = p.Name};
                    command.Parameters.Add(par);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }

    public class QueryParameter
    {
        public readonly string Name;
        public readonly object Value;
        public Type ValueType;

        public QueryParameter(string name, object value, Type valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }
    }
}
