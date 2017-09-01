using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using LittleBlog.Exceptions;

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

            entity.Tags = GetTags(entity);

            UnitOfWork.ArticleRepository.Add(entity);
            
            UnitOfWork.Commit();
        }

        public IEnumerable<GetArticleDTO> ShowArticles()
        {
            return Mapper.Map<IEnumerable<Article>, 
                IEnumerable<GetArticleDTO>>(UnitOfWork.ArticleRepository.GetAll());
        }

        public IEnumerable<GetArticleDTO> ShowPreviewArticle(int startWith=0, int count=0, int countOfWords=100)
        {
            var entities = UnitOfWork.ArticleRepository.GetAll().ToList();
            
            var dtos = Mapper.Map<IEnumerable<Article>, IEnumerable<GetArticleDTO>>(
                entities);
            
            return dtos.Select(a => {
                    a.Description = String.Join(" ", a.Description.Split(' ').Take(countOfWords)); return a;
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

        public ImageDTO GetFileByName(string name)
        {
            string[] ext = {".jpg", ".jpeg", ".png", ".gif"};

            ImageDTO image = Mapper.Map<Image, ImageDTO>(
                UnitOfWork.ArticleRepository.GetAll()
                    .SelectMany(a => a.Images).FirstOrDefault(i => 
                        Path.GetFileNameWithoutExtension(i.ImageUrl) == name)
            );
            
            if (image == null)
            {
                throw FileException.FileNameNotExists(name);
            }

            return image;
        } 

        private ICollection<Tag> GetTags(Article entity)
        {
            var listOfTags = new List<Tag>();
            
            var dbTags = this.UnitOfWork.TagRepository.GetAll().ToList();
            
            var entityTags = entity.Tags;

            if (!dbTags.Any())
            {
                listOfTags.AddRange(entityTags);
            }
            else
            {
                foreach (var tag in entityTags)
                {
                    foreach (var tag1 in dbTags)
                    {
                        listOfTags.Add(tag1.Name == tag.Name ? tag1 : tag);
                    }
                }
            }
            
            return listOfTags;
        }

        public int CountArticles() => UnitOfWork.ArticleRepository.Count();
    }
}