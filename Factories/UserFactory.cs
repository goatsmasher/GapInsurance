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
                dbConnection.Open();
                bool makeadmin = FirstUser();
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                item.password = Hasher.HashPassword(item, item.password);
                if (makeadmin == false)
                {
                    string query = "INSERT INTO user (first_name, last_name, email, password, admin, created_at, updated_at) VALUES (@first_name, @last_name, @email, @password, 1, NOW(), NOW())";
                    dbConnection.Execute(query, item);
                    return dbConnection.Query<User>(query, item).FirstOrDefault();
                }
                else
                {
                    string query = "INSERT INTO user (first_name, last_name, email, password, admin, created_at, updated_at) VALUES (@first_name, @last_name, @email, @password, 0, NOW(), NOW())";
                    dbConnection.Execute(query, item);
                    return dbConnection.Query<User>(query, item).FirstOrDefault();
                }
            }
        }
        
        public User GetUser(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<User>("SELECT * FROM users WHERE id = @id", new { id = id }).FirstOrDefault();
            }
        }
        public User Login(string email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE email = @email", new { email = email }).FirstOrDefault();
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
        public bool FirstUser()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open(); //do i need to open this connection again if i only use it inside of another connection??
                object result = dbConnection.Query<User>("SELECT * FROM user", new { admin = 1 }).FirstOrDefault(); ;
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
    }
}