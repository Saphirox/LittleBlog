using AutoMapper;
using LittleBlog.DAL.Repositories;

namespace LittleBlog.BLL.Services
{
    public interface IService
    {
        IUnitOfWork UnitOfWork { get; }
        IMapper Mapper { get; }
    }
}