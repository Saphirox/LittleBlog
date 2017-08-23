using System.Collections.Generic;
using LittleBlog.Dtos.Shared;
using LittleBlog.Entities.Article;

namespace LittleBlog.Dtos.Article
{
    public class ArticleDTO : DTO
    {
        public string Header { get; set; }

        public int TimeForRead { get; set; }

        public string Description { get; set; }

        public ICollection<ImageDTO> Images { get; set; }

        public int Viewers { get; set; }

        public ICollection<PublishEditDateDTO> PublishEditDates { get; set; }

        public ICollection<TagDTO> Tags { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}