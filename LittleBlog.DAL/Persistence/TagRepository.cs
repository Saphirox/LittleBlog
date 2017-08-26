using System.Collections.Generic;
using System.Data.Entity;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Persistence
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(Context context) : base(context)
        {
        }

        /// <summary>
        /// Get tags without articles 
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Tag> GetAll()
        {
            return DbContext.Tags;
        }

        public IEnumerable<Tag> GetAllIncludeArticles()
        {
            return DbContext.Tags.Include(t => t.Articles);
        }
    }
}