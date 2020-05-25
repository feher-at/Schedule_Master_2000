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
               (int)reader["userid"],
               (int)reader["slotid"],
               (string)reader["title"],
               (string)reader["content"],
               (int)reader["lenght"]);
              

        }

        private readonly IDbConnection _connection;

        public SqlTaskService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Tasks> GetOneUserAllTasks(int userID)
        {
            List<Tasks> tasks = new List<Tasks>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tasks WHERE userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userID;
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(ToTasks(reader));
            }

            return tasks;
        }

        //public List<Tasks> GetOneSlotAllTasks(int slotID)
        //{
        //    List<Tasks> tasks = new List<Tasks>();
        //    using var command = _connection.CreateCommand();
        //    command.CommandText = "SELECT * FROM tasks WHERE slotid = @slotid";

        //    var param = command.CreateParameter();
        //    param.ParameterName = "slotid";
        //    param.Value = slotID;

        //    using var reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        tasks.Add(ToTasks(reader));
        //    }

        //    return tasks;
        //}

        public Tasks GetOne(int taskID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * from tasks WHERE taskid = @taskid";
            var param = command.CreateParameter();
            param.ParameterName = "taskid";
            param.Value = taskID;
            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();

            reader.Read();
            return ToTasks(reader);
        }

        public void DeleteTask(int taskID,int userID)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "Select delete_task(@userid, @taskid)";

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userid";
            userIdParam.Value = userID;
            var taskIdParam = command.CreateParameter();
            taskIdParam.ParameterName = "taskid";
            taskIdParam.Value = taskID;
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(taskIdParam);
            HandleExecuteNonQuery(command);
        }

        public void DeleteAllTasks(int userID)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = $"DELETE * FROM tasks WHERE userid = @userid";
            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userid";
            userIdParam.Value = userID;
            command.Parameters.Add(userIdParam);
            HandleExecuteNonQuery(command);
        }

        public void UpdateTask(int userID, int taskID, string title, string content)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userId";
            userIDParam.Value = userID;

            var taskIDParam = command.CreateParameter();
            taskIDParam.ParameterName = "taskid";
            taskIDParam.Value = taskID;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            

            var contentParam = command.CreateParameter();
            contentParam.ParameterName = "content";
            contentParam.Value = (object)content ?? DBNull.Value;

            

            command.CommandText = "SELECT update_task(@userId, @taskid, @title, @content )";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(taskIDParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contentParam);
            HandleExecuteNonQuery(command);

            
        }

        public void InsertTask(int userID, string title, string content)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userid";
            userIDParam.Value = userID;

            var TitleParam = command.CreateParameter();
            TitleParam.ParameterName = "title";
            TitleParam.Value = title;

            var ContentParam = command.CreateParameter();
            ContentParam.ParameterName = "content";
            ContentParam.Value = content;

            

            command.CommandText = $"INSERT INTO tasks(userid,title,content) VALUES (@userid, @title, @content)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(TitleParam);
            command.Parameters.Add(ContentParam);
            
            HandleExecuteNonQuery(command);
        }

    }
}
