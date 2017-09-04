using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IArticleService : ICounterService, IPreviewArticleService
    {
        void AddArticle(CreateArticleDTO articleDto);
        
        GetArticleDTO GetArticleById(int id);
        
        void UpdateArticle(GetArticleDTO getArticleDto);
        
        void DeleteArticle(int id);

        IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags);
    }
}