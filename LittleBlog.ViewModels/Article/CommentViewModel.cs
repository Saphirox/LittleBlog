using System;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Article
{
    public class CommentViewModel : ViewModel
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }
    }
}