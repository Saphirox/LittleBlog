using LittleBlog.Entities.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.Tests
{
    public static class FakeTags
    {
        public static IList<Tag> CreateTags()
        {
            List<Tag> tags = new List<Tag>();
            tags.Add(new Tag() { Id = 1, Name = "Hello1" });
            tags.Add(new Tag() { Id = 2, Name = "Hello2" });
            tags.Add(new Tag() { Id = 3, Name = "Hello3" });
            tags.Add(new Tag() { Id = 4, Name = "Hello4" });
            tags.Add(new Tag() { Id = 5, Name = "Hello5" });
            tags.Add(new Tag() { Id = 6, Name = "Hello6" });
            return tags;
        }
    }
}
