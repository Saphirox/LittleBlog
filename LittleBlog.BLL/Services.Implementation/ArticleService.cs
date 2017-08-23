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
    public class ArticleService : IArticleService
    {
        private IUnitOfWork UnitOfWork { get; }

        public ArticleService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void AddArticle(ArticleDTO articleDto)
        {
            this.AddRemainderTags(Mapper.Map<IEnumerable<TagDTO>, IEnumerable<Tag>>(articleDto.Tags));
            
            var entity = Mapper.Map<ArticleDTO, Article>(articleDto);

            entity.PublishEditDates.Add(new PublishEditDate { Date = DateTime.UtcNow });

            UnitOfWork.ArticleRepository.Add(entity);
            UnitOfWork.Commit();
        }

        public IEnumerable<ArticleDTO> ShowArticles()
        {
            return Mapper.Map<IEnumerable<Article>, 
                IEnumerable<ArticleDTO>>(UnitOfWork.ArticleRepository.GetAll());
        }

        public IEnumerable<ArticleDTO> ShowPreviewArticle(int startWith, int count)
        {
            return Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(
                UnitOfWork.ArticleRepository.GetAll()
            ).Skip(startWith).Take(count);
        }

        public ArticleDTO GetArticeById(int id)
        {
            return Mapper.Map<Article, ArticleDTO>(
                UnitOfWork.ArticleRepository.GetById(id)
            );
        }

        public void UpdateArticle(ArticleDTO articleDto)
        {
            UnitOfWork.ArticleRepository.Update(Mapper.Map<ArticleDTO, Article>(articleDto));
            UnitOfWork.Commit();
        }

        public void DeleteArticle(int id)
        {
           throw new NotImplementedException();
        }

        public IEnumerable<ArticleDTO> GetArticlesBy(Func<ArticleDTO, bool> expression)
        {
            return
                Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(
                    UnitOfWork.ArticleRepository.GetAll()
                ).Where(expression);
        }

        public IEnumerable<ArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags)
        {
            return
                Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(
                    UnitOfWork.ArticleRepository.GetAll()
                ).Where(x => x.Tags.Intersect((ICollection<TagDTO>)tags).Count() == tags.Count());
        }

        #region Helpers

        public void AddRemainderTags(IEnumerable<Tag> source)
        {
            foreach (var tag in source.Distinct().Except(this.UnitOfWork.TagRepository.GetAll()))
            {
                this.UnitOfWork.TagRepository.Add(tag);
            }
        }

        #endregion
    }
}