using LittleBlog.DAL.Persistence.Configuration;
using LittleBlog.Entities.Article;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistence
{
    using LittleBlog.Entities.Shared;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Context : IdentityDbContext<AppUser>
    {
        public Context()
            : base("name=Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Log> Logs { get; set; }
    }
}