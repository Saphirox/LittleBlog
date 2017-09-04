using System.Threading.Tasks;
using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        protected Context DbContext { get; set; }

        public UnitOfWork(Context dbContext)
        {
            DbContext = dbContext;
        }

        public int Commit()
        {
            return this.DbContext.SaveChanges();
        }
        
        public async Task<int> CommitAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
    }
}