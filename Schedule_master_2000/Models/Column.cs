using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Column
    {
        public int ColumnID { get; private set; }
        public int ScheduleID { get; private set; }
        public string Title { get; set; }
        

        public Column(int columnID,int scheduleID,string title)
        {
            ColumnID = columnID;
            ScheduleID = scheduleID;
            Title = title;
            
        }


    }
}
