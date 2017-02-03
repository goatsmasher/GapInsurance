using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public void AddProduct(Product item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"INSERT INTO products (name, cost, category, description, img_src, created_at, updated_at) VALUES (@name, @cost, @category, @description, @img_src, NOW(), NOW())";
                dbConnection.Execute(query, item);
            }
        }
        public Product GetProduct(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<Product>("SELECT * FROM products WHERE id = @id", new { id = id }).FirstOrDefault();
            }
        }
        public IEnumerable<Product> Related_Coverages()
        {
            using (IDbConnection dbConnection = Connection) 
            {
                return dbConnection.Query<Product>("SELECT * FROM products ORDER BY created_at DESC LIMIT 3");
            }
        }
    }
}