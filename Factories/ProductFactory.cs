using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using stupid.Models;
using stupid.ViewModels;

namespace stupid.Factory
{
    public class ProductFactory : IFactory<Product>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public ProductFactory(IOptions<MySqlOptions> conf)
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
        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<Product>("SELECT * FROM products");

            }
        }
        public void AddProduct(ProductViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO products (name, cost, catagory, description, img_src, created_at, updated_at) VALUES (@name, @cost, @catagory, @description, @img_src, NOW(), NOW())";
                dbConnection.Execute(query, item);
            }
        }
    }
}