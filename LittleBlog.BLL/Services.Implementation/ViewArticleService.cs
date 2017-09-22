using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Dtos.Article;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Article;
using AutoMapper;

namespace LittleBlog.BLL.Services.Implementation
{
    public class ViewArticleService : Service<IArticleUnitOfWork>, IViewArticleService
    {
        public ViewArticleService(IArticleUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {}

        public GetArticleDTO GetArticleById(int id)
        {
            return Mapper.Map<Article, GetArticleDTO>(
                UnitOfWork.ArticleRepository.GetById(id)
            );
        }

        public IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> dtoTags)
        {
            var tags = Mapper.Map<IEnumerable<TagDTO>, IEnumerable<Tag>>(dtoTags);

            return Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                UnitOfWork.ArticleRepository.GetAll()
                    .Where(a => a.Tags.Any(s => tags.Contains(s))));
        }

        public IEnumerable<GetArticleDTO> GetPreviewArticles(int startWith = 0, int count = 0, int countOfWords = 0)
        {
            if (count == 0)
            {
                return Array.Empty<GetArticleDTO>();
            }

            var entities = UnitOfWork.ArticleRepository.GetAll();

            var dtos = Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                entities);

            return dtos.Select(a => {
                a.Description = string.Join(" ", a.Description.Split(' ').Take(countOfWords)); return a;
            }).Skip(startWith).Take(count);

        }

        public int CountArticles() => UnitOfWork.ArticleRepository.GetAll().Count();
    }
}
