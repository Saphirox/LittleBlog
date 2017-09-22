using LittleBlog.Dtos.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.BLL.Services
{
    public interface IViewArticleService : IPreviewArticleService, ICounterService
    {
        GetArticleDTO GetArticleById(int id);

        IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags);
    }
}
