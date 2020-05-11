﻿using Schedule_master_2000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Services
{
    interface IUserService
    {
        User GetOne(int id);
        List<User> GetAll();

    }
}
