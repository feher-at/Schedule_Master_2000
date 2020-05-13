using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public interface IScheduleService
    {
        List<Schedule> GetOneUserAllSchedule(int userID);
        Schedule GetOne(int scheduleID);
        List<Schedule> GetAll();
        void DeleteSchedule(int userID, int scheduleID);
        void UpdateSchedule(int userID, int ScheduleID, string title);
        void InsertSchedule(int userID, string title);
    }
}
