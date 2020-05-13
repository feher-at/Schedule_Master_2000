using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Tasks
    {
        public int TaskID { get; private set; }
        public int UserID { get; private set; }
        public string Title { get; set; }
     
        public string Content { get; set; }
        

        public Tasks(int taskID,int userID,string title, string content)
        {
            TaskID = taskID;
            UserID = userID;
            Title = title;
            Content = content;
            

        }

     
        
    }
}
