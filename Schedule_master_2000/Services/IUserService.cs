using Schedule_master_2000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Services
{
    public interface IUserService
    {
        User GetOne(int userid);
        User GetOne(string email);
        void InsertUser(string userName, string password, string email, string role = "user");
        void DeleteUser(int id);
        void UpdateUser(int id, string username, string password, string email);

        bool CheckIfUserExists(string email);
        bool ValidateUser(string email, string password);

        User GetOneUserByEmail(string email);
    }
}
