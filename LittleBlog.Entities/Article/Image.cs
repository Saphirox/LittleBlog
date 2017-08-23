using LittleBlog.Entities.Shared;

namespace LittleBlog.Entities.Article
{
    public class Image : Entity
    {
        public string ImageUrl { get; set; }

        public Article Article { get; set; }
    }
}