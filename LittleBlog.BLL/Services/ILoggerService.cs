using LittleBlog.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.BLL.Services
{
    public interface ILoggerService
    {
        void Log(LogStatus logStatus, string message, string source);
    }
}
