
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataContent
{
    public class DapperOrmHelper : IDapperOrmHelper
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; set; }
        public string ProviderName { get; }
        public DapperOrmHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionString = _configuration.GetConnectionString("DBConnection");
            ProviderName = "System.Data.SqlClient";
        }


        public IDbConnection GetDapperContextHelper()
        {
            return new SqlConnection(ConnectionString);
        }

    }

}
