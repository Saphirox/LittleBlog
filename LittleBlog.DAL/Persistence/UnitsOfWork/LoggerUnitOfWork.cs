using LittleBlog.DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.DAL.Repositories;

namespace LittleBlog.DAL.Persistence.UnitsOfWork
{
    public class LoggerUnitOfWork : UnitOfWork, ILoggerUnitOfWork
    {
        public LoggerUnitOfWork(
            Context dbContext, 
            ILoggerRepository loggerRepository) 
            : base(dbContext)
        {
            LoggerRepository = loggerRepository;
        }

        public ILoggerRepository LoggerRepository { get; set; }
    }
}
