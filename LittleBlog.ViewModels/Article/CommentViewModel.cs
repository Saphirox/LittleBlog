using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Article
{
    public class CommentViewModel : ViewModel
    {
        [Required]
        public string Author { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }
        
        [HiddenInput]
        // When post request would happen -> ignore this field
        public DateTime DateTime { get; set; }
    }
}