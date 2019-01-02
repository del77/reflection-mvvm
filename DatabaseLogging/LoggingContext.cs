using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogging
{
    public class LoggingContext : DbContext
    {
        public LoggingContext() : base(@"LogsDatabase")
        {
        }

        public DbSet<Log> Logs { get; set; }
    }
}
