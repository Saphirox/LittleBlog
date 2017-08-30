using System;
using System.Collections;
using System.Collections.Generic;
using LittleBlog.DAL.Persistence;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetAll();
        IEnumerable<Article> GetArticlesByTag(IEnumerable<Tag> tags);
        IEnumerable<Article> GetArticleByDateTimes(DateTime start, DateTime end);

        int Count();
    }
}