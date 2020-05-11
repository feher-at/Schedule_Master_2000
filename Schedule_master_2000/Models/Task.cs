using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Task
    {
        public int TaskID { get; private set; }
        public int SlotID { get; private set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgPath { get; set; }

        public Task(int taskID, int slotID,string title, string content, string imgPath)
        {
            TaskID = taskID;
            SlotID = slotID;
            Title = title;
            Content = content;
            ImgPath = imgPath;

        }
    }
}
