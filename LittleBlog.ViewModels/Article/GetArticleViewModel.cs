using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Article
{
    public class ArticleViewModel : ViewModel
    {
        public string Header { get; set; }

        public int TimeForRead { get; set; }

        public string Description { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public int Viewers { get; set; }

        public ICollection<PublishEditDateViewModel> PublishEditDates { get; set; }

        public ICollection<TagViewModel> Tags { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
