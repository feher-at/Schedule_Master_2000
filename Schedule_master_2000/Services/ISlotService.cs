using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Services
{
    public interface ISlotService
    {
        List<Slot> GetOneColumnAllSlots(int columnID);
        Slot GetOne(int slotID);
        List<Slot> GetOneUsersAllSlots(int userID);
        void InsertSlot(int columnID, int userID, int hour);
    }
}
