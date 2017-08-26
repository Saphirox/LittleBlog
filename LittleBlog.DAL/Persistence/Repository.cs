using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Shared;

namespace LittleBlog.DAL.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: Entity
    {
        private Context _dbContext;

        public Context DbContext
        {
            get => _dbContext;
            set => _dbContext = value;
        }

        public Repository(Context context)
        {
            this._dbContext = context;
        }
     
        public void Add(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Added;  
        }

        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                //if (DbContext.Entry<TEntity>(entity).State == EntityState.Detached)
                //{
                //    DbContext.Set<TEntity>().Attach(entity);
                //}
                DbContext.Set<TEntity>().Remove(entity);
                //DbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
        }
    }
}
