using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Entities.Shared;

namespace LittleBlog.Entities.Article
{
    public class PublishEditDate : Entity
    {
        public DateTime Date { get; set; }

        public Article Article { get; set; }
    }
}
