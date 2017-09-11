using System.Collections.Generic;
using AutoMapper;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Article;
using LittleBlog.Mapper;
using Moq;
using NUnit.Framework;

namespace LittleBlog.Tests
{
    [TestFixture]
    public class FileTests
    {
        private Mock<IArticleRepository> _mock;
        private IArticleUnitOfWork _articleUnitOfWork;
        private FileService _fileService;
        private IMapper _mapper;

        
        private static readonly Article article = new Article()
        {
            Description = "Lorem ipsum else some count",
            Header = "Lorem",
            Id = 1,
            TimeForRead = 20,
            Images = new List<Image>() { new Image() {ImageUrl = "file.jpg"} }
        };
        
        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IArticleRepository>();
            _articleUnitOfWork = new ArticleUnitOfWork(null, _mock.Object, null);
            _mapper = MapperBuilder.BuildMapper();
            _fileService = new FileService(_articleUnitOfWork, _mapper);
        }
        
        [Test]
        public void GetFileByName_Test()
        {
            _mock.Setup(setup => setup.GetAll()).Returns(new[] {article});
            Assert.AreEqual("file.jpg", _fileService.GetFileByName("file").ImageUrl);
        } 
    }
}