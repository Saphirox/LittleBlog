using System.Collections.Generic;
using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IPreviewArticleService
    {
        IEnumerable<GetArticleDTO> GetPreviewArticles(int startWith, int count, int countOfWords);
    }
}