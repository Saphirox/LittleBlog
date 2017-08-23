using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Entities.Shared;

namespace LittleBlog.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        void Add(TEntity entity);
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
