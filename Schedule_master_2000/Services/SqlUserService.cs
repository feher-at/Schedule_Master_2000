using Npgsql;
using Schedule_master_2000.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Services
{
    public class SqlUserService: SqlBaseService, IUserService
    {
        private static User ToUser(IDataReader reader)
        {
            return new User
            (
               (int)reader["userid"],
               (string)reader["username"],
               (string)reader["user_password"],
               (string)reader["email"],
               (string)reader["user_role"]

            );
        }

        private readonly IDbConnection _connection;

        public SqlUserService(IDbConnection connection)
        {
            _connection = connection;
        }

        public User GetOne(int userid)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM users WHERE userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userid;
            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
            reader.Read();
            return ToUser(reader);
        }
        public User GetOne(string email)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM users WHERE email = '{email}'";

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToUser(reader);
        }

        public List<User> GetAll()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM users";

            using var reader = command.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(ToUser(reader));
            }
            return users;
        }

        public void DeleteUser(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Delete * From users Where userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = id;
            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
        }

        public User Login(string username, string password)
        {
            using var command = _connection.CreateCommand();

            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "username";
            usernameParam.Value = username;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = password;
            command.CommandText = $"SELECT * FROM users WHERE username = @username AND user_password = @password";
            command.Parameters.Add(usernameParam);
            command.Parameters.Add(passwordParam);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return ToUser(reader);
            }
            return null;
        }

        public void Register(string userName, string password, string email, string role)
        {
            using var command = _connection.CreateCommand();

            var userNameParam = command.CreateParameter();
            userNameParam.ParameterName = "username";
            userNameParam.Value = userName;
            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = password;
            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;
            var roleParam = command.CreateParameter();
            roleParam.ParameterName = "role";
            roleParam.Value = role;

            command.CommandText = $"INSERT INTO users(username,user_password,email,user_role) VALUES (@username, @password, @email, @role)";
            command.Parameters.Add(userNameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(emailParam);
            command.Parameters.Add(roleParam);

            HandleExecuteNonQuery(command);

        }

        public bool CheckIfUserExists(string email)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT true FROM users WHERE email = '{email}'";
            var param = command.CreateParameter();
            param.ParameterName = "email";
            param.Value = email;
            bool UserExist = Convert.ToBoolean(command.ExecuteScalar());

            return UserExist;
        }
    }
}

