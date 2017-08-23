using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LittleBlog.Entities.Shared;

namespace LittleBlog.Entities.Article
{
    public class Article : Entity
    {
        public string Header { get; set; }

        public int TimeForRead { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }

        public int Viewers { get; set; }

        public ICollection<PublishEditDate> PublishEditDates { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}