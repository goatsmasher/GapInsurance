using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using stupid.Models;
namespace stupid.Factory
{
    public class CartFactory : IFactory<Cart>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public CartFactory(IOptions<MySqlOptions> conf)
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
        public IEnumerable<Cart> GetCart(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<Cart>("SELECT * FROM carts WHERE Users_id = @id", new { id = id });

            }
        }
        public void AddToCart(int product, int user)
        { //need to also pass the current id for logged in user
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute($"INSERT INTO carts (Products_id, Users_id, created_at, updated_at) VALUES ({product}, {user}, NOW(), NOW());");
            }
        }
        public void Checkout()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(""); //this will run a query that will assign the current products in the cart to the current logged in user
            }
        }

    }
}