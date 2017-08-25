using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;

namespace LittleBlog.BLL.Services.Implementation
{
    public class ArticleService : Service, IArticleService
    {
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
           : base (unitOfWork, mapper)
        {}

        public void AddArticle(CreateArticleDTO articleDto)
        {
            var entity = Mapper.Map<CreateArticleDTO, Article>(articleDto);
            entity.Tags = this.GetRemainderTags(entity.Tags);

            UnitOfWork.ArticleRepository.Add(entity);
            UnitOfWork.Commit();
        }

        public IEnumerable<GetArticleDTO> ShowArticles()
        {
            return Mapper.Map<IEnumerable<Article>, 
                IEnumerable<GetArticleDTO>>(UnitOfWork.ArticleRepository.GetAll());
        }

        public IEnumerable<GetArticleDTO> ShowPreviewArticle(int startWith=0, int count=0)
        {
            var entities = UnitOfWork.ArticleRepository.GetAll().ToList();
            var dtos = Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                entities);
            return dtos.Skip(startWith).Take(count);
            
        }

        public GetArticleDTO GetArticleById(int id)
        {
            return Mapper.Map<Article, GetArticleDTO>(
                UnitOfWork.ArticleRepository.GetById(id)
            );
        }

        public void UpdateArticle(GetArticleDTO getArticleDto)
        {
            UnitOfWork.ArticleRepository.Update(Mapper.Map<GetArticleDTO, Article>(getArticleDto));
            UnitOfWork.Commit();
        }

        public void DeleteArticle(int id)
        {
            var entity = UnitOfWork.ArticleRepository.GetById(id);
            UnitOfWork.ArticleRepository.Delete(entity);
            UnitOfWork.Commit();
        }

        public IEnumerable<GetArticleDTO> GetArticlesBy(Func<GetArticleDTO, bool> expression)
        {
            return
                Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                    UnitOfWork.ArticleRepository.GetAll()
                ).Where(expression);
        }

        public IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags)
        {
            return
                Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                    UnitOfWork.ArticleRepository.GetAll())
                    .Where(x => x.Tags.Intersect((ICollection<TagDTO>)tags).Count() == tags.Count());
        }

        #region Helpers

        public ICollection<Tag> GetRemainderTags(ICollection<Tag> source)
        {
            return source.Distinct()
                         .Except(this.UnitOfWork.TagRepository.GetAll()).ToList();
        }

        #endregion
    }
}