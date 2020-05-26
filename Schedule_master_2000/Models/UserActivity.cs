using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class UserActivity
    {
        public int UserID { get; set; }
        public string Activity { get; set; }

        public DateTime ActivityTime { get; set; }


        public UserActivity(int userID, string activity, DateTime activityTime)
        {
            UserID = userID;
            Activity = activity;
            ActivityTime = activityTime;
        }
    }
}
