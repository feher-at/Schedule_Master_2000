using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public interface ITaskService
    {
        List<Tasks> GetOneUserAllTasks(int userID);
        List<Tasks> GetOneSlotAllTasks(int slotID);
        Tasks GetOne(int taskID);
        void DeleteTask(int taskID,int userID);

    }
}
