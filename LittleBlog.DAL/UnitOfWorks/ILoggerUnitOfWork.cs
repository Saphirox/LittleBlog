using LittleBlog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.DAL.UnitOfWorks
{
    public interface ILoggerUnitOfWork : IUnitOfWork
    {
        ILoggerRepository LoggerRepository { get; set; }
    }
}
