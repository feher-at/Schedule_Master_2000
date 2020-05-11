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



    }
}
