using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LittleBlog.DAL.Repositories;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Persistance
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(Context context) : base(context)
        {
        }

        public IEnumerable<Article> GetAll()
        {
            return DbContext.Articles
                .Include(b => b.Comments)
                .Include(b => b.Images)
                .Include(b => b.PublishEditDates);
        }

        public IEnumerable<Article> GetArticlesByTag(IEnumerable<Tag> tags)
        {
            return this.GetAll().Where(el => el.Tags.Intersect(tags).Count() == tags.Count());
        }

        public IEnumerable<Article> GetArticleByDateTimes(DateTime start, DateTime end)
        {
            return this.GetAll().Where(el => 
                    el.PublishEditDates.Last().Date > start && 
                    el.PublishEditDates.Last().Date < end);
        }

    }
}