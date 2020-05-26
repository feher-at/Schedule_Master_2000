using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlUserActivityService:SqlBaseService,IUserActivityService
    {
        private static UserActivity ToUserActivity(IDataReader reader)
        {
            return new UserActivity(
               (int)reader["userid"],
               (string)reader["activity"],
               (DateTime)reader["activity_time"]

                );
        }

        private readonly IDbConnection _connection;

        public SqlUserActivityService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<UserActivity> GetAllActivity()
        {
            List<UserActivity> activities = new List<UserActivity>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM activities ";

            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                activities.Add(ToUserActivity(reader));
            }

            return activities;
        }

        public void InsertActivity(int userID,string activity,DateTime activityTime)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userid";
            userIDParam.Value = userID;

            var activityParam = command.CreateParameter();
            activityParam.ParameterName = "activity";
            activityParam.Value = activity;

            var dateParam = command.CreateParameter();
            dateParam.ParameterName = "activityTime";
            dateParam.Value = activityTime;



            command.CommandText = $"INSERT INTO activities(userid,activity,activity_time) VALUES (@userid, @activity, @activityTime)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(activityParam);
            command.Parameters.Add(dateParam);

            HandleExecuteNonQuery(command);
        }

    }
}
