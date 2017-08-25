using AutoMapper;
using LittleBlog.DAL.Repositories;

namespace LittleBlog.BLL.Services.Implementation
{
    public abstract class Service 
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        protected Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;    
        }
    }
}