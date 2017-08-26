using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using LittleBlog.Entities.Article;

namespace LittleBlog.DAL.Persistence.Configuration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasMany<Tag>(a => a.Tags)
                .WithMany(t => t.Articles);

            HasMany<PublishEditDate>(a => a.PublishEditDates)
                .WithRequired(p => p.Article);

            HasMany<Image>(a => a.Images)
                .WithRequired(p => p.Article);
        }
    }
}