using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.SqlConnectionObject
{
    public class DBConnection
    {
        private static SqlConnection sqlConnecion;
        private readonly string sqlConnectionString;
        private readonly IConfiguration configuration;
        public DBConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DBConnection");
            sqlConnecion = new SqlConnection(sqlConnectionString);
        }
        public static SqlConnection GetConnection()
        {
            return sqlConnecion;
        }

    }
}
