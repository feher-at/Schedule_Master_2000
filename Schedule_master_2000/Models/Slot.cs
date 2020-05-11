using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class Slot
    {
        public int SlotID { get; private set; }
        public int ColumnID { get; private set; }

        public Slot(int slotID,int columnID)
        {
            SlotID = slotID;
            ColumnID = columnID;
        }
    }
}
