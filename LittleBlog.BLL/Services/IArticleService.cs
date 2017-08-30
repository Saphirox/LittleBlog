using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IArticleService
    {
        void AddArticle(CreateArticleDTO articleDto);

        IEnumerable<GetArticleDTO> ShowArticles();
        
        IEnumerable<GetArticleDTO> ShowPreviewArticle(int startWith, int count, int countOfWords);
        
        GetArticleDTO GetArticleById(int id);
        
        void UpdateArticle(GetArticleDTO getArticleDto);
        
        void DeleteArticle(int id);

        int CountArticles();

        IEnumerable<GetArticleDTO> GetArticlesBy(Func<GetArticleDTO, bool> expression);
        
        IEnumerable<GetArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags);
    }
}