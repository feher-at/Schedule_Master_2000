using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Tasks
    {
        public int TaskID { get; private set; }
        public int SlotID { get; private set; }
        public int ScheduleID { get; private set; }
        public int UserID { get; private set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public string Content { get; set; }
        public string ImgPath { get; set; }

        public Tasks(int taskID, int slotID,int scheduleID,int userID,string title, DateTime date, int hour, string content, string imgPath)
        {
            TaskID = taskID;
            SlotID = slotID;
            ScheduleID = scheduleID;
            UserID = userID;
            Title = title;
            Date = date;
            Hour = hour;
            Content = content;
            ImgPath = imgPath;

        }

     
        
    }
}
