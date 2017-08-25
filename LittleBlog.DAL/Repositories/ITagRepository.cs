using System.Collections;
using System.Collections.Generic;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetAll();
        IEnumerable<Tag> GetAllIncludeArticles();
    }
}