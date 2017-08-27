using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Article
{
    public class CreateArticleViewModel : ViewModel
    {
        [Required]
        [DisplayName("Title")]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Header { get; set; }

        [Required]
        [DisplayName("Time for reading article (average)")]
        [AllowHtml]
        [DataType(DataType.Time)]
        public int TimeForRead { get; set; }

        [Required]
        [DisplayName("Description")]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Enter tags(if tag have space, please, wrap tag into quotes)")]
        [DataType(DataType.Text)]
        [AllowHtml]
        public string Tags { get; set; }
    }
}