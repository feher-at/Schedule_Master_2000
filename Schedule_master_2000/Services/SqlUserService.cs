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

    }
}

