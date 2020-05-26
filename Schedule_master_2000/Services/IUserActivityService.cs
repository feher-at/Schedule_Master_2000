using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public interface IUserActivityService
    {
        List<UserActivity> GetAllActivity();
        void InsertActivity(int userID, string activity, DateTime activityTime);
    }
}
