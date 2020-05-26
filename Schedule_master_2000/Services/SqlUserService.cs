using Npgsql;
using Schedule_master_2000.Domain;
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
        private static User ToExistingUser(IDataReader reader)
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
            return ToExistingUser(reader);
        }

        public User GetOne(string email)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM users WHERE email = '{email}'";

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToExistingUser(reader);
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

        public bool ValidateUser(string email, string password)
        {
            using var command = _connection.CreateCommand();

            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "user_password";
            passwordParam.Value = Utility.Hash(password);
            command.CommandText = $"SELECT * FROM users WHERE email = @email AND user_password = @user_password";
            command.Parameters.Add(emailParam);
            command.Parameters.Add(passwordParam);

            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return true;
            }

            return false;
        }

        public void InsertUser(string userName, string password, string email, string role)
        {
            using var command = _connection.CreateCommand();

            var userNameParam = command.CreateParameter();
            userNameParam.ParameterName = "username";
            userNameParam.Value = userName;
            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "user_password";
            passwordParam.Value = Utility.Hash(password);
            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;
            var roleParam = command.CreateParameter();
            roleParam.ParameterName = "user_role";
            roleParam.Value = role;

            command.CommandText = $"INSERT INTO users(username, user_password, email, user_role) VALUES (@username, @user_password, @email, @user_role)";
            command.Parameters.Add(userNameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(emailParam);
            command.Parameters.Add(roleParam);

            HandleExecuteNonQuery(command);
        }

        public User GetOneUserByEmail(string email)
        {
            using var command = _connection.CreateCommand();

            var userNameParam = command.CreateParameter();
            userNameParam.ParameterName = "email";
            userNameParam.Value = email;

            command.CommandText = $"SELECT* FROM users WHERE email = @email";
            command.Parameters.Add(userNameParam);

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToExistingUser(reader);
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

        public void UpdateUser(int id, string username, string password, string email)
        {
            using var command = _connection.CreateCommand();
            var idParam = command.CreateParameter();
            idParam.ParameterName = "userid";
            idParam.Value = id;
            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "username";
            usernameParam.Value = username;
            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;

            if (password == null)
            {
                command.CommandText = $"UPDATE users SET username = @username, email = @email WHERE userid = @userid";
                command.Parameters.Add(idParam);
                command.Parameters.Add(usernameParam);
                command.Parameters.Add(emailParam);
            }
            else
            {
                var passwordParam = command.CreateParameter();
                passwordParam.ParameterName = "user_password";
                passwordParam.Value = Utility.Hash(password);

                command.CommandText = $"UPDATE users SET username = @username, user_password = @user_password, email = @email WHERE userid = @userid";
                command.Parameters.Add(idParam);
                command.Parameters.Add(usernameParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(emailParam);
            }

            HandleExecuteNonQuery(command);
        }
    }
}

