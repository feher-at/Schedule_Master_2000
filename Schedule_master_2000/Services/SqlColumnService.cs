using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlColumnService : SqlBaseService,IColumnService
    {
        private static Column ToColumn(IDataReader reader)
        {
            return new Column(
               (int)reader["schedule_columnsid"],
               (int)reader["userid"],
               (int)reader["scheduleid"],
               (string)reader["title"] );
           
        }

        private readonly IDbConnection _connection;

        public SqlColumnService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Column> GetAllCollumnToOneUser(int userID)
        {
            List<Column> column = new List<Column>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM schedule_columns WHERE userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                column.Add(ToColumn(reader));
            }

            return column;
        }

        public List<Column> GetOneScheduleAllColumn(int scheduleID)
        {
            List<Column> column = new List<Column>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM schedule_columns WHERE scheduleid = @scheduleid";

            var param = command.CreateParameter();
            param.ParameterName = "scheduleid";
            param.Value = scheduleID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                column.Add(ToColumn(reader));
            }

            return column;
        }

        public Column GetOne(int columnID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * from schedule_columns WHERE schedule_columnsid = @columnid";
            var param = command.CreateParameter();
            param.ParameterName = "columnid";
            param.Value = columnID;
            using var reader = command.ExecuteReader();

            reader.Read();
            return ToColumn(reader);
        }

        public void DeleteColumn(int columnID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Delete * From schedule_columns Where schedule_columnsid = @columnid";

            var param = command.CreateParameter();
            param.ParameterName = "columnid";
            param.Value = columnID;
            using var reader = command.ExecuteReader();
        }

        public void InsertColumn(int scheduleID, int userID,string title)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userid";
            userIDParam.Value = userID;
            var scheduleIDParam = command.CreateParameter();
            scheduleIDParam.ParameterName = "scheduleid";
            scheduleIDParam.Value = scheduleID;
            var TitleParam = command.CreateParameter();
            TitleParam.ParameterName = "title";
            TitleParam.Value = title;
            command.CommandText = $"INSERT INTO schedule_columns(scheduleid,userid,title) VALUES (@scheduleid ,@userid, @title)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(scheduleIDParam);
            command.Parameters.Add(TitleParam);
            HandleExecuteNonQuery(command);
        }

        public void UpdateColumn(int userID,int columnID,string title)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userId";
            userIDParam.Value = userID;

            var columnIDParam = command.CreateParameter();
            columnIDParam.ParameterName = "columnid";
            columnIDParam.Value = columnID;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            command.CommandText = "SELECT update_column(@userId, @columnid, @title)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(columnIDParam);
            command.Parameters.Add(titleParam);
            HandleExecuteNonQuery(command);
        }
    }
}
