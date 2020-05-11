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
        Schedule GetOne(int id);

        List<Schedule> GetAll();
        void DeleteSchedule(int id);
    }
}
