using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Entities.Shared;

namespace LittleBlog.Entities.Article
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
