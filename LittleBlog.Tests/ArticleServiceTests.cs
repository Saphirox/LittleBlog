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
        private Mock<IArticleUnitOfWork> _articleUnitOfWork;
        private Mock<IArticleUnitOfWork> _fakeArticleUnitOfWork;
        private ArticleService _sut;
        private IMapper _mapper;
        private Mock<ITagRepository> _fakeTagRepository;

        private Article FakeArticle;
        
        [SetUp]
        public void SetUp()
        {
            FakeArticle = FakeArticles.CreateArticleWithOneTagAndOneDate();

            this._fakeArticleRepository = new Mock<IArticleRepository>();
           
            this._fakeTagRepository = new Mock<ITagRepository>();

            _fakeArticleUnitOfWork = new Mock<IArticleUnitOfWork>();

            this._articleUnitOfWork = new Mock<IArticleUnitOfWork>();

            this._articleUnitOfWork.Setup(s => s.ArticleRepository).Returns(_fakeArticleRepository.Object);

            this._articleUnitOfWork.Setup(s => s.TagRepository).Returns(_fakeTagRepository.Object);

            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] { FakeArticle });


            _mapper = MapperBuilder.BuildMapper();
            
            this._sut = new ArticleService(_articleUnitOfWork.Object, _mapper);
        }

        [Test]
        public void Counter_Test()
        {

            Assert.AreEqual(_sut.CountArticles(), 1);
        }
        
        [Test]
        public void FindingById_Test()
        {
            var result = _sut.GetArticleById(1);
             
            Assert.NotNull(result);
            Assert.AreEqual(FakeArticle.Id, result.Id);
        }

        [Test]
        public void GetArticlesByTags_Test()
        {
            FakeArticle.Tags = FakeArticles.CreateTags();

            var article = _sut.GetArticlesByTags(FakeArticles.CreateTagsDto()).First();
            
            Assert.NotNull(article);
            Assert.AreEqual(article.Header, FakeArticle.Header);
        }

        [Test]
        public void GetPreviewArticlesOneArticle_Test()
        {
            const int count = 1;
            const int startWith = 0;
            const int words = 3;
            
            var article = _sut.GetPreviewArticles(startWith, count, words).First();
            
            Assert.AreEqual(article.Header, FakeArticle.Header);
            Assert.AreEqual(article.Description, "Lorem ipsum else");
        }
        
        [Test]
        public void GetPreviewArticlesNoArticle_Test()
        {
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] {FakeArticle});

            // Count of articles
            const int count = 0;
            
            // Start with article
            const int startWith = 0;

            // Count of words which would be display in preview mode
            const int words = 3;
            
            var article = _sut.GetPreviewArticles(startWith, count, words);
            
            Assert.NotNull(article);
            Assert.IsTrue(!article.Any());
        }

        [Test]
        public void AddArticleWithoutTags_Test()
        {
            this._sut = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);

            var article = FakeArticles.CreateArticleWithTwoTagsDto();

            List<Article> dbArticles = new List<Article>();

            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _fakeTagRepository.Setup(s => s.GetAll()).Returns(Array.Empty<Tag>()).Verifiable();

            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1);
            
            _sut.AddArticle(article);
            
            Assert.NotZero(dbArticles.Count);
            
            Assert.AreEqual(2, article.Tags.Count);
        }

        [Test]
        public void AddArticleIfTagsExists_Test()
        {
            var dbArticles = new List<Article>();
            var dbTags = new List<Tag>();
            var tags = FakeArticles.CreateTags();

            dbTags.AddRange(tags);

            this._sut = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);

            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();
           
            _sut.AddArticle(FakeArticles.CreateArticleWithTwoTagsDto());
            
            Assert.AreEqual(2, dbArticles[0].Tags.Count);
            
            Assert.AreSame(dbArticles[0].Tags.ToList()[0], tags[0]);
            Assert.AreSame(dbArticles[0].Tags.ToList()[1], tags[1]);
        }

        [Test]
        public void UpdateArticle_Test()
        {
            var dbArticles = new List<Article>();
            
            this._sut = new ArticleService(_fakeArticleUnitOfWork.Object, _mapper);

            CreateArticleDTO article = FakeArticles.CreateArticleWithTwoTagsDto();
            
            _fakeArticleRepository.Setup(m => m.Add(It.IsAny<Article>()))
                .Callback((Article a) => { dbArticles.Add(a); }).Verifiable();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();
            
            _fakeArticleRepository.Setup(setup => setup.Update(It.IsAny<Article>())).Callback((Article a) =>
            {
                dbArticles.Remove(a);
                dbArticles.Add(a);
            });
            
            _sut.AddArticle(article);

            GetArticleDTO getArticleDto = FakeArticles.CreateGetArticle();

            _sut.UpdateArticle(getArticleDto);
            
            Assert.AreEqual(dbArticles[0].Header, getArticleDto.Header);
        }

        [Test]
        public void DeleteArticle_Test()
        {
            var dbArticles = new List<Article>();
            
            CreateArticleDTO article = FakeArticles.CreateArticleWithTwoTagsDto();
            
            _fakeArticleUnitOfWork.Setup(setup => setup.Commit()).Returns(1).Verifiable();

            _fakeArticleRepository.Setup(setup => setup.GetById(2)).Returns(FakeArticles.CreateArticle());
            
            _fakeArticleRepository.Setup(setup => setup.Delete(It.IsAny<Article>())).Callback((Article a) =>
            {dbArticles.Remove(a);});
            
            _sut.AddArticle(article);
            
            _sut.DeleteArticle(article.Id);
            
            Assert.IsTrue(!dbArticles.Any());
        }
    }
}