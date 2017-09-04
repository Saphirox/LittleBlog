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
    public class ArticleServiceTests
    {
        private Mock<IArticleRepository> _fakeArticleRepository;
        private IArticleUnitOfWork _articleUnitOfWork;
        private Mock<IArticleUnitOfWork> _fakeArticleUnitOfWork;
        private ArticleService _articleService;
        private IMapper _mapper;
        private Mock<ITagRepository> _mockTagRepository;

        private static readonly Article article = new Article()
        {
            Description = "Lorem ipsum else some count",
            Header = "Lorem",
            Id = 1,
            TimeForRead = 20,
        };
        
        [SetUp]
        public void SetUp()
        {
            this._fakeArticleRepository = new Mock<IArticleRepository>();
           
            this._mockTagRepository = new Mock<ITagRepository>();
            
            this._articleUnitOfWork = new ArticleUnitOfWork(null, _fakeArticleRepository.Object, _mockTagRepository.Object);
            
            _mapper = MapperBuilder.BuildMapper();
            
            this._articleService = new ArticleService(_articleUnitOfWork, _mapper);

            _fakeArticleUnitOfWork = new Mock<IArticleUnitOfWork>();

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

        [Test]
        public void GetPreviewArticlesOneArticleTest()
        {
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] {article});

            const int count = 1;
            const int startWith = 0;
            const int words = 3;
            
            var l = _articleService.GetPreviewArticles(startWith, count, words).First();
            
            Assert.AreEqual(l.Header, article.Header);
            Assert.AreEqual(l.Description, "Lorem ipsum else");
        }
        
        [Test]
        public void GetPreviewArticlesNoArticleTest()
        {
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] {article});

            const int count = 0;
            const int startWith = 0;
            const int words = 3;
            
            var l = _articleService.GetPreviewArticles(startWith, count, words);
            
            Assert.NotNull(l);
            Assert.IsTrue(!l.Any());
        }

        [Test]
        public void AddArticleTest()
        {
            this._articleService = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);
            
            CreateArticleDTO article = new CreateArticleDTO()
            {
                Description = "Lorem ipsum else some count",
                Header = "Lorem",
            };
            
            article.Tags = new[] {new TagDTO { Name = "Hello" }, new TagDTO {Name = "World"} };
            
            List<Article> dbArticles = new List<Article>();

            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _mockTagRepository.Setup(s => s.GetAll()).Returns(Array.Empty<Tag>()).Verifiable();

            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1);
            
            _fakeArticleUnitOfWork.Setup(setup => setup.TagRepository).Returns(_mockTagRepository.Object);
            _fakeArticleUnitOfWork.Setup(setup => setup.ArticleRepository).Returns(_fakeArticleRepository.Object);
            
            _articleService.AddArticle(article);
            
            Assert.NotZero(dbArticles.Count);
            
            Assert.AreEqual(2, article.Tags.Count);
        }

        [Test]
        public void AddArticleIfTagsExistsTest()
        {
            var dbArticles = new List<Article>();
            var dbTags = new List<Tag>();

            var one1 = new Tag {Name = "One"};
            var two2 = new Tag {Name = "Two"};
            dbTags.Add(one1);
            dbTags.Add(two2);
            
            this._articleService = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);
            
            CreateArticleDTO article = new CreateArticleDTO()
            {
                Description = "Lorem ipsum else some count else description porko rosso",
                Header = "Lorem",
            };
            
            article.Tags = new[] {new TagDTO { Name = "One" }, new TagDTO {Name = "Two"} };    
            
            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _mockTagRepository.Setup(s => s.GetAll()).Returns(dbTags).Verifiable();

            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.TagRepository).Returns(_mockTagRepository.Object);
            _fakeArticleUnitOfWork.Setup(setup => setup.ArticleRepository).Returns(_fakeArticleRepository.Object);

           
            
            _articleService.AddArticle(article);
            
            Assert.AreEqual(2, dbArticles[0].Tags.Count);
            
            Assert.AreSame(dbArticles[0].Tags.ToList()[0], one1);
            Assert.AreSame(dbArticles[0].Tags.ToList()[1], two2);
        }

        [Test]
        public void UpdateArticleTest()
        {
            var dbArticles = new List<Article>();
            
            this._articleService = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);
            
            CreateArticleDTO article = new CreateArticleDTO()
            {
                Id = 2,
                Description = "Lorem ipsum else some count else description porko rosso",
                Header = "Lorem",
            };
            
            article.Tags = new[] {new TagDTO { Name = "One" }, new TagDTO {Name = "Two"} };   
            
            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.TagRepository).Returns(_mockTagRepository.Object);
            _fakeArticleUnitOfWork.Setup(setup => setup.ArticleRepository).Returns(_fakeArticleRepository.Object);
            
            _fakeArticleRepository.Setup(setup => setup.Update(It.IsAny<Article>())).Callback((Article a) =>
            {
                dbArticles.Remove(a);
                dbArticles.Add(a);
            });
            
            _articleService.AddArticle(article);
            
            _fakeArticleUnitOfWork.Setup(setup => setup.ArticleRepository).Returns(_fakeArticleRepository.Object);
            
            _articleService.UpdateArticle(new GetArticleDTO() { Id = 2, Header = "Lorem ipsum"});
            
            Assert.AreEqual(dbArticles[0].Header, "Lorem ipsum");
        }

        [Test]
        public void DeleteArticleTest()
        {
            var dbArticles = new List<Article>();
            
            this._articleService = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);
            
            CreateArticleDTO article = new CreateArticleDTO()
            {
                Id = 2,
                Description = "Lorem ipsum else some count else description porko rosso",
                Header = "Lorem",
            };
            
            article.Tags = new[] {new TagDTO { Name = "One" }, new TagDTO {Name = "Two"} };   
            
            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();

            _fakeArticleRepository.Setup(setup => setup.GetById(2)).Returns(new Article()
            {
                Id = 2,
                Description = "Lorem ipsum else some count else description porko rosso",
                Header = "Lorem",
            });
            
            _fakeArticleRepository.Setup(setup => setup.Delete(It.IsAny<Article>())).Callback((Article a) =>
            {
                dbArticles.Remove(a);
            });
            
            _fakeArticleUnitOfWork.Setup(setup => setup.TagRepository).Returns(_mockTagRepository.Object);
            _fakeArticleUnitOfWork.Setup(setup => setup.ArticleRepository).Returns(_fakeArticleRepository.Object);
            
            _articleService.AddArticle(article);
            
            _articleService.DeleteArticle(2);
            
            Assert.IsTrue(!dbArticles.Any());
        }
    }
}