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
               (int)reader["scheduleid"],
               (int)reader["schedule_columnsid"],
               (int)reader["userid"],
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

        public List<Tasks> GetOneUserAllTasks(int userID)
        {
            List<Tasks> tasks = new List<Tasks>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tasks WHERE slotid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(ToTasks(reader));
            }

            return tasks;
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

        public void UpdateTask(int userID, int taskID, string title, DateTime date, int hour,string content,string imgPath)
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

            var dateParam = command.CreateParameter();
            dateParam.ParameterName = "date";
            dateParam.Value = (object)date ?? DBNull.Value;

            var hourParam = command.CreateParameter();
            hourParam.ParameterName = "hour";
            hourParam.Value = hour;

            var contentParam = command.CreateParameter();
            contentParam.ParameterName = "content";
            contentParam.Value = (object)content ?? DBNull.Value;

            var imgPathParam = command.CreateParameter();
            imgPathParam.ParameterName = "imgPath";
            imgPathParam.Value = (object)imgPath ?? DBNull.Value;

            command.CommandText = "SELECT update_task(@userId, @taskid, @title, @date, @hour, @content, @imgPath)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(taskIDParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(dateParam);
            command.Parameters.Add(hourParam);
            command.Parameters.Add(contentParam);
            command.Parameters.Add(imgPathParam);
            HandleExecuteNonQuery(command);

            
        }

        public void InsertTask(int slotID, int scheduleID,int userID,int columnID, string title, DateTime date, int hour, string content,string imgPath)
        {
            using var command = _connection.CreateCommand();

            var slotIDParam = command.CreateParameter();
            slotIDParam.ParameterName = "slotid";
            slotIDParam.Value = slotID;

            var scheduleIDParam = command.CreateParameter();
            scheduleIDParam.ParameterName = "scheduleid";
            scheduleIDParam.Value = scheduleID;

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userid";
            userIDParam.Value = userID;

            var columnIDParam = command.CreateParameter();
            columnIDParam.ParameterName = "columnid";
            columnIDParam.Value = columnID;

            var TitleParam = command.CreateParameter();
            TitleParam.ParameterName = "title";
            TitleParam.Value = title;

            var DateParam = command.CreateParameter();
            DateParam.ParameterName = "date";
            DateParam.Value = date;

            var HourParam = command.CreateParameter();
            HourParam.ParameterName = "hour";
            HourParam.Value = hour;

            var ContentParam = command.CreateParameter();
            ContentParam.ParameterName = "content";
            ContentParam.Value = content;

            var imgPathParam = command.CreateParameter();
            imgPathParam.ParameterName = "imgPath";
            imgPathParam.Value = imgPath;

            command.CommandText = $"INSERT INTO tasks(userid,scheduleid,schedule_columnsid,slotid,taskdate,taskhour,title,content,img) VALUES (@userid, @scheduleid, @columnid, @slotid, @date, @hour, @title, @content, @imgPath)";
            command.Parameters.Add(slotIDParam);
            command.Parameters.Add(scheduleIDParam);
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(columnIDParam);
            command.Parameters.Add(TitleParam);
            command.Parameters.Add(DateParam);
            command.Parameters.Add(HourParam);
            command.Parameters.Add(ContentParam);
            command.Parameters.Add(imgPathParam);
            HandleExecuteNonQuery(command);
        }

    }
}
