using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.DAL.Persistence.Repositories
{
    public class LoggerRepository : Repository<Log>, ILoggerRepository
    {
        public LoggerRepository(Context context) : base(context)
        {
        }
    }
}
