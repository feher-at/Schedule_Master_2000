using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    interface IColumnService
    {
        List<Column> GetOneScheduleAllColumn(int scheduleID);
        Column GetOne(int columnID);
        void DeleteColumn(int columnID);
    }
}
