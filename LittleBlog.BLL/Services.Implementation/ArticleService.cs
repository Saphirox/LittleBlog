using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using LittleBlog.BLL.Infrastructure;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using LittleBlog.Exceptions;

namespace LittleBlog.BLL.Services.Implementation
{
    public class ArticleService : Service<IArticleUnitOfWork>, IArticleService
    {
        public ArticleService(IArticleUnitOfWork unitOfWork, IMapper mapper)
           : base (unitOfWork, mapper)
        {}

        public void AddArticle(CreateArticleDTO articleDto)
        {
            var entity = Mapper.Map<CreateArticleDTO, Article>(articleDto);

            var tagUtil = new TagUtil(UnitOfWork.TagRepository);
            
            entity.Tags = tagUtil.GetTags(entity);

            UnitOfWork.ArticleRepository.Add(entity);
            
            UnitOfWork.Commit();
        }

        public IEnumerable<GetArticleDTO> GetPreviewArticles(int startWith=0, int count=0, int countOfWords=0)
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

        public IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> dtoTags)
        {
            var tags = Mapper.Map<IEnumerable<TagDTO>, IEnumerable<Tag>>(dtoTags);
            
            return Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                UnitOfWork.ArticleRepository.GetAll()
                    .Where(a => a.Tags.Any(s => tags.Contains(s))));
        }

        public int CountArticles() => UnitOfWork.ArticleRepository.GetAll().Count();
    }
}