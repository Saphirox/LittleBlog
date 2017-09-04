using System.Threading.Tasks;
using LittleBlog.DAL.Repositories;

namespace LittleBlog.DAL.UnitOfWorks
{
    public interface IUnitOfWork
    {
        
        int Commit();
       
        Task<int> CommitAsync();
    }
}