using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlTaskService : SqlBaseService, ITaskService
    {
        private static Tasks ToTasks(IDataReader reader)
        {
            return new Tasks(
               (int)reader["taskid"],
               (int)reader["slotid"],
               (string)reader["title"],
               (DateTime)reader["taskdate"],
               (int)reader["taskhour"],
               (string)reader["content"],
               (string)reader["img"]);

        }

        private readonly IDbConnection _connection;

        public SqlTaskService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Tasks> GetOneSlotAllTasks(int slotID)
        {
            List<Tasks> tasks = new List<Tasks>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tasks WHERE slotid = @slotid";

            var param = command.CreateParameter();
            param.ParameterName = "slotid";
            param.Value = slotID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(ToTasks(reader));
            }

            return tasks;
        }

        public Tasks GetOne(int taskID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * from tasks WHERE taskid = @taskid";
            var param = command.CreateParameter();
            param.ParameterName = "taskid";
            param.Value = taskID;
            using var reader = command.ExecuteReader();

            reader.Read();
            return ToTasks(reader);
        }

        public void DeleteTask(int taskID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Delete * From tasks Where taskid = @taskid";

            var param = command.CreateParameter();
            param.ParameterName = "taskid";
            param.Value = taskID;
            using var reader = command.ExecuteReader();
        }

    }
}
