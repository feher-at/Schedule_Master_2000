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
            {
                Id = (int)reader["id"],
                Username = (string)reader["username"],
                RegistrationTime = (DateTime)reader["registration_time"],
                QuestionCount = (long)reader["question_count"],
                AnswerCount = (long)reader["answer_count"],
                CommentCount = (long)reader["comment_count"],
            };
        }

        private readonly IDbConnection _connection;

        public SqlUserService(IDbConnection connection)
        {
            _connection = connection;
        }

        public User GetOne(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM v_user WHERE id = @id";

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToUser(reader);
        }

        public List<User> GetAll()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM v_user";

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

