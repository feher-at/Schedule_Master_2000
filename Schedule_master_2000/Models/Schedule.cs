using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Schedule_master_2000.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        
        public string Title { get; set; }

        public int UserID { get; set; }

        public Schedule(int scheduleID,string title, int userID)
        {
            ScheduleID = scheduleID;
            Title = title;
            UserID = userID;

        }
        public Schedule(string title, int userID)
        {
            Title = title;
            UserID = userID;

        }

        public Schedule()
        {

        }
    }
}
