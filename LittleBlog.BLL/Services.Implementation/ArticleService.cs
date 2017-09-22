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
            var article = Mapper.Map<CreateArticleDTO, Article>(articleDto);

            var tagUtil = new TagUtil(UnitOfWork.TagRepository);
            
            article.Tags = tagUtil.GetTags(article);

            UnitOfWork.ArticleRepository.Add(article);
            
            UnitOfWork.Commit();
        }

        public void UpdateArticle(GetArticleDTO articleDto)
        {
            UnitOfWork.ArticleRepository.Update(Mapper.Map<GetArticleDTO, Article>(articleDto));
            
            UnitOfWork.Commit();
        }

        public void DeleteArticle(int id)
        {
            var article = UnitOfWork.ArticleRepository.GetById(id);
            
            UnitOfWork.ArticleRepository.Delete(article);
            
            UnitOfWork.Commit();
        }

    }
}