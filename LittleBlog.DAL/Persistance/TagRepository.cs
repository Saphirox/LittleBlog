using System.Collections.Generic;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Persistance
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(Context context) : base(context)
        {
        }

        public IEnumerable<Tag> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}