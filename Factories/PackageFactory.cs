using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using stupid.Models;
namespace stupid.Factory
{
    public class PackageFactory : IFactory<Package>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public PackageFactory(IOptions<MySqlOptions> conf)
        {
            mysqlConfig = conf;
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public IEnumerable<Package> GetCart(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<Package>("SELECT * FROM packages");

            }
        }
    }
}