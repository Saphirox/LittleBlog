using System;
using System.Collections.Generic;
using LittleBlog.Dtos.Shared;

namespace LittleBlog.Dtos.Article
{
    public class CreateArticleDTO : DTO
    {
        public string Header { get; set; }

        public int TimeForRead { get; set; }

        public string Description { get; set; }
        
        public DateTime PublishDate { get; set; }

        public ICollection<ImageDTO> Images { get; set; }

        public ICollection<TagDTO> Tags { get; set; }
    }
}