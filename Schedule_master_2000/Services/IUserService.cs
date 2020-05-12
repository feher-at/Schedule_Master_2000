﻿using Schedule_master_2000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Services
{
    public interface IUserService
    {
        User GetOne(int userid);
        List<User> GetAll();

        void DeleteUser(int id);


    }
}
