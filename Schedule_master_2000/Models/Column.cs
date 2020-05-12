using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Column
    {
        public int ColumnID { get; private set; }
        public int UserID { get; private set; }
        public int ScheduleID { get; private set; }
        public string Title { get; set; }
        

        public Column(int columnID,int userID,int scheduleID,string title)
        {
            ColumnID = columnID;
            UserID = userID;
            ScheduleID = scheduleID;
            Title = title;
            
        }


    }
}
