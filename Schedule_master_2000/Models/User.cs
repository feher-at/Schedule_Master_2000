using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class User
    {
        public int UserID { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }

        public string Roll { get; set; }
        
        public List<Schedule> Schedule { get; set; }


        public User(int userID, string username, string password, string email, DateTime registrationTime, string roll, List<Schedule> schedule)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Email = email;
            RegistrationTime = registrationTime;
            Roll = roll;
            Schedule = schedule;
        }

    }
}
