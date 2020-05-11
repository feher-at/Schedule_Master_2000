using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public class SqlScheduleService : SqlBaseService, IScheduleService
    {
        private static Schedule ToSchedule(IDataReader reader)
        {
            return new Schedule(

                );
        }
    }
}
