using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Article
{
    public class CreateArticleViewModel : ViewModel
    {
        [Required]
        [DisplayName("Title")]
        [DataType(DataType.Html)]
        public string Header { get; set; }

        [Required]
        [DisplayName("Time for reading article (average)")]
        [DataType(DataType.Time)]
        public int TimeForRead { get; set; }

        [Required]
        [DisplayName("Description")]
        [DataType(DataType.Html)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string Tags { get; set; }
    }
}