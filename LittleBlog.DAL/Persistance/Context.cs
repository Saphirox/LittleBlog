using LittleBlog.Entities.Article;
using LittleBlog.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.DAL.Persistance
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Context : IdentityDbContext<AppUser>
    {
        public Context()
            : base("name=Context")
        {}

        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Image> Images { get; set; }
    }
}