using System;
using LittleBlog.Entities.Shared;

namespace LittleBlog.Entities.Article
{
    public class Comment : Entity
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public Article Article { get; set; }
    }
}