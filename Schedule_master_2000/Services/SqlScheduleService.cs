using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlScheduleService : SqlBaseService, IScheduleService
    {
        private static Schedule ToSchedule(IDataReader reader)
        {
            return new Schedule(
               (int)reader["schedule"],
               (string)reader["title"],
               (int)reader["userid"]
               
                );
        }

        private readonly IDbConnection _connection;

        public SqlScheduleService(IDbConnection connection)
        {
            _connection = connection;
        }
        public List<Schedule> GetOneUserAllSchedule(int userID)
        {
            List<Schedule> schedules = new List<Schedule>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM schedules WHERE userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userID;

            using var reader = command.ExecuteReader();
            while(reader.Read())
            {
                schedules.Add(ToSchedule(reader));
            }
            
            return schedules;
        }

        public List<Schedule> GetAll()
        {
            List<Schedule> schedules = new List<Schedule>();
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * From schedules";
            using var reader = command.ExecuteReader();
            while(reader.Read())
            {
                schedules.Add(ToSchedule(reader));
            }
            return schedules;
        }

        public Schedule GetOne(int scheduleID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * from schedules WHERE scheduleid = @scheduleid";
            var param = command.CreateParameter();
            param.ParameterName = "scheduleid";
            param.Value = scheduleID;
            using var reader = command.ExecuteReader();
            
            reader.Read();
            return ToSchedule(reader);
        }

        public void DeleteSchedule(int userID, int scheduleID)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "Select delete_schedule(@userid, @scheduleid)";

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userid";
            userIdParam.Value = userID;
            var scheduleIdParam = command.CreateParameter();
            scheduleIdParam.ParameterName = "scheduleid";
            scheduleIdParam.Value = scheduleID;
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(scheduleIdParam);
            HandleExecuteNonQuery(command);

        }

        public void InsertSchedule(int userID, string title)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userid";
            userIdParam.Value = userID;
            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = title;
            command.CommandText = $"INSERT INTO schedules(userid,title) VALUES (@userid, @title)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(titleParam);
            HandleExecuteNonQuery(command);
        }

        public void UpdateSchedule(int userID, int scheduleID,string title)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userId";
            userIDParam.Value = userID;

            var scheduleIDParam = command.CreateParameter();
            scheduleIDParam.ParameterName = "scheduleid";
            scheduleIDParam.Value = scheduleID;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            command.CommandText = "SELECT update_schedule(@userId, @scheduleid, @title)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(scheduleID);
            command.Parameters.Add(titleParam);
            HandleExecuteNonQuery(command);
        }

            
    }
}
