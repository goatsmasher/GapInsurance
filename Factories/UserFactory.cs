using stupid.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using stupid.ViewModels;

namespace stupid.Factory
{
    public class UserFactory : IFactory<User>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
        public UserFactory(IOptions<MySqlOptions> conf)
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
        public void Add(User item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO users (first_name, last_name, username, password, created_at, updated_at) VALUES (@first_name, @last_name, @username, @password, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public User AddWithReturn(RegisterViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                item.password = Hasher.HashPassword(item, item.password);
                string query = "INSERT INTO users (first_name, last_name, username, password, created_at, updated_at) VALUES (@first_name, @last_name, @username, @password, NOW(), NOW()); SELECT LAST_INSERT_ID() as id";
                dbConnection.Open();
                return dbConnection.Query<User>(query, item).FirstOrDefault();
            }
        }
        public User GetUser(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<User>("SELECT * FROM users WHERE id = @id", new { id = id }).FirstOrDefault();
            }
        }
        public User Login(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE username = @username", new { username = username }).FirstOrDefault();
            }
        }
        public IEnumerable<User> AllUsers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * from users");
            }
        }
    }
}