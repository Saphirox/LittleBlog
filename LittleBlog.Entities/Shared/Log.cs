using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.Entities.Shared
{
    public class Log : Entity
    {
        public string Source { get; set; }

        public LogStatus LogStatus { get; set; }

        public string Message { get; set; }
    }
}
