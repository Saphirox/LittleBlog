using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.ViewModels.Article
{
    public class PreviewArticlesViewModel
    {
        public PreviewArticlesViewModel(IEnumerable<GetArticleViewModel> articlesViewModels, int countAllArticles)
        {
            ArticlesViewModels = articlesViewModels;
            CountAllArticles = countAllArticles;
        }

        public IEnumerable<GetArticleViewModel> ArticlesViewModels { get; set; }
        public int CountAllArticles { get; set; }
    }
}
