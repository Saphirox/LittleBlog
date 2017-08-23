using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IArticleService
    {
        void AddArticle(ArticleDTO articleDto);
        IEnumerable<ArticleDTO> ShowArticles();
        IEnumerable<ArticleDTO> ShowPreviewArticle(int startWith, int count);
        ArticleDTO GetArticeById(int id);
        void UpdateArticle(ArticleDTO articleDto);
        void DeleteArticle(int id);
        IEnumerable<ArticleDTO> GetArticlesBy(Func<ArticleDTO, bool> expression);
        IEnumerable<ArticleDTO> GetArticlesByTags(IEnumerable<TagDTO> tags);
    }
}