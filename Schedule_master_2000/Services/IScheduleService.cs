using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    interface IScheduleService
    {
        List<Schedule> GetOneUserAllSchedule(int userID);
        Schedule GetOne(int scheduleID);

        List<Schedule> GetAll();
        void DeleteSchedule(int userID, int scheduleID);
    }
}
