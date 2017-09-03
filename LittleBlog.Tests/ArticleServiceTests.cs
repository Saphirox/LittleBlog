using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using LittleBlog.Mapper;
using Moq;
using NUnit.Framework;

namespace LittleBlog.Tests
{
    [TestFixture]
    public class FakeArticleService
    {
        private Mock<IArticleRepository> _fakeArticleRepository;
        private IArticleUnitOfWork _fakeArticleUnitOfWork;
        private ArticleService _articleService;
        private IMapper _mapper;

        private static Article article = new Article()
        {
            Description = "Lorem ipsum",
            Header = "Lorem",
            Id = 1,
            TimeForRead = 20,
        };
        
        
        
            
        [SetUp]
        public void SetUp()
        {
            this._fakeArticleRepository = new Mock<IArticleRepository>();
            
            this._fakeArticleUnitOfWork = new ArticleUnitOfWork(null ,_fakeArticleRepository.Object);
            
            _mapper = MapperBuilder.BuildMapper();
            
            this._articleService = new ArticleService(_fakeArticleUnitOfWork, _mapper);

            article.PublishEditDates = new List<PublishEditDate>()
            {
                new PublishEditDate() { Article = article, Id = 1, Date = DateTime.UtcNow }
            };
            
            article.Tags = new List<Tag>()
            {
                new Tag() { Name = "Hello" }
            };

        }

        [Test]
        public void CounterTest()
        {
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] {article});

            Assert.AreEqual(_articleService.CountArticles(), 1);
        }
        
        [Test]
        public void FindingByIdTest()
        {
            _fakeArticleRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns( article );

            var result = _articleService.GetArticleById(1);
             
            Assert.NotNull(result);
            Assert.AreEqual(article.Id, result.Id);
        }

        [Test]
        public void GetArticlesByTagsTest()
        {
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] {article});
            
            var l = _articleService.GetArticlesByTags(new[] {new TagDTO() {Name = "Hello"}}).First();
            
            Assert.NotNull(l);
            Assert.AreEqual(l.Header, article.Header);
        }
        
        public v
    }
}