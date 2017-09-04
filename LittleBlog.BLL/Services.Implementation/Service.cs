using AutoMapper;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;

namespace LittleBlog.BLL.Services.Implementation
{
    public abstract class Service<TUnitOfWork> where TUnitOfWork: IUnitOfWork
    {
        protected TUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        protected Service(TUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}