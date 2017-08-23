using System;
using LittleBlog.Dtos.Shared;
using LittleBlog.Entities.Shared;

namespace LittleBlog.Dtos.Article
{
    public class CommentDTO : DTO
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }
    }
}