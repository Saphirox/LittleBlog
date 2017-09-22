using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Article;
using LittleBlog.Mapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.Tests
{
    public class ViewArticleServiceTest
    {

        private Mock<IArticleRepository> _fakeArticleRepository;
        private Mock<IArticleUnitOfWork> _articleUnitOfWork;
        private Mock<IArticleUnitOfWork> _fakeArticleUnitOfWork;
        private IViewArticleService _sut;
        private IMapper _mapper;
        private Mock<ITagRepository> _fakeTagRepository;

        private Article FakeArticle;

        [SetUp]
        public void Setup()
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

            this._sut = new ViewArticleService(_articleUnitOfWork.Object, _mapper);
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
            _fakeArticleRepository.Setup(s => s.GetAll()).Returns(new[] { FakeArticle });

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
        public void Counter_Test()
        {
            Assert.AreEqual(_sut.CountArticles(), 1);
        }
    }
}
