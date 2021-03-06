﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class OneUserSchedules
    {
        public User User { get; set; }

        public List<Schedule> Schedules { get; set; }
        public List<Column> Columns { get; set;  }
        public List<Slot> Slots { get; set; }
        public List<Tasks> Tasks { get; set; }

        public OneUserSchedules(User user, List<Schedule> schedules, List<Column> columns, List<Slot> slots, List<Tasks> tasks)
        {
            User = user;
            Schedules = schedules;
            Columns = columns;
            Slots = slots;
            Tasks = tasks;
        }
    }
}
