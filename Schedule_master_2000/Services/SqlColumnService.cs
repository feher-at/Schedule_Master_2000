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
               (int)reader["scheduleid"],
               (string)reader["title"] );
           
        }

        private readonly IDbConnection _connection;

        public SqlColumnService(IDbConnection connection)
        {
            _connection = connection;
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
    }
}
