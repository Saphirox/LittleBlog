using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Entities.Shared;
using LittleBlog.DAL.UnitOfWorks;
using AutoMapper;

namespace LittleBlog.BLL.Services.Implementation
{
    public class LoggerService : Service<ILoggerUnitOfWork>, ILoggerService
    {
        public LoggerService(
            ILoggerUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {}

        public void Log(LogStatus logStatus, string message, string source)
        {
            var logEntity = new Log() { LogStatus = logStatus, Message = message, Source = source };

            UnitOfWork.LoggerRepository.Add(logEntity);

            UnitOfWork.Commit();
        }
    }
}
