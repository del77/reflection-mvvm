using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogging
{
    [Export(typeof(LoggingContext))]
    public class LoggingContext : DbContext
    {


        public DbSet<Log> Logs { get; set; }
    }
}
