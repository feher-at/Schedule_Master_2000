using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlSlotService: SqlBaseService,ISlotService
    {
        private static Slot ToSlot(IDataReader reader)
        {
            return new Slot(
               (int)reader["slotid"],
               (int)reader["schedule_columnsid"],
               (int)reader["userid"],
               (int)reader["slot_hour"]);




        }

        private readonly IDbConnection _connection;

        public SqlSlotService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Slot> GetOneUsersAllSlots(int userID)
        {
            List<Slot> slot = new List<Slot>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM slots WHERE userid = @userid";

            var param = command.CreateParameter();
            param.ParameterName = "userid";
            param.Value = userID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                slot.Add(ToSlot(reader));
            }

            return slot;
        }

        public List<Slot> GetOneColumnAllSlots(int columnID)
        {
            List<Slot> slot = new List<Slot>();
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM slots WHERE schedule_columnsid = @columnid";

            var param = command.CreateParameter();
            param.ParameterName = "columnid";
            param.Value = columnID;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                slot.Add(ToSlot(reader));
            }

            return slot;
        }

        public Slot GetOne(int slotID)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Select * from slot WHERE slotid = @slotid";
            var param = command.CreateParameter();
            param.ParameterName = "columnid";
            param.Value = slotID;
            using var reader = command.ExecuteReader();

            reader.Read();
            return ToSlot(reader);
        }

        public void DeleteAllColumn(int userID)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = $"DELETE * FROM slots WHERE userid = @userid";
            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userid";
            userIdParam.Value = userID;
            command.Parameters.Add(userIdParam);
            HandleExecuteNonQuery(command);
        }

        public void InsertSlot(int columnID, int userID,int hour)
        {
            using var command = _connection.CreateCommand();

            var userIDParam = command.CreateParameter();
            userIDParam.ParameterName = "userid";
            userIDParam.Value = userID;
            var columnIDParam = command.CreateParameter();
            columnIDParam.ParameterName = "columnid";
            columnIDParam.Value = columnID;

            var hourParam = command.CreateParameter();
            hourParam.ParameterName = "hour";
            hourParam.Value = hour;
            command.CommandText = $"INSERT INTO slots(schedule_columnsid, userid,slot_hour) VALUES (@columnid, @userid, @hour)";
            command.Parameters.Add(userIDParam);
            command.Parameters.Add(columnIDParam);
            command.Parameters.Add(hourParam);
            HandleExecuteNonQuery(command);
        }

    }
}
