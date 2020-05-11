using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Schedule_master_2000.Models
{
    public class Schedule
    {
        public int ScheduleID { get; private set; }
        
        public string Title { get; set; }

        public int UserID { get; private set; }

        public Schedule(int scheduleID,string title, int userID)
        {
            ScheduleID = scheduleID;
            Title = title;
            UserID = userID;

        }
    }
}
