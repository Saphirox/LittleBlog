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
    
        void UpdateArticle(GetArticleDTO getArticleDto);
        
        void DeleteArticle(int id);
    }
}