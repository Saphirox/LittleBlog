using LittleBlog.DAL.Repositories;

namespace LittleBlog.DAL.UnitOfWorks
{
    public interface ITagUnitOfWork
    {
        ITagRepository TagRepository { get; set; }
    }
}