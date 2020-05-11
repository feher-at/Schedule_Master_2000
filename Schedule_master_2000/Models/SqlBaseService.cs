using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class SqlBaseService
    {
        protected static readonly string dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        protected static readonly string dbUser = Environment.GetEnvironmentVariable("DB_USER");
        protected static readonly string dbPass = Environment.GetEnvironmentVariable("DB_PASS");
        protected static readonly string dbName = Environment.GetEnvironmentVariable("DB_NAME");
        public static readonly string connectingString = $"Host={dbHost};Username={dbUser};Password={dbPass};Database={dbName}";
    }
}
