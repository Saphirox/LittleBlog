using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LittleBlog.BLL.Infrastructure;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Article;
using Moq;
using NUnit.Framework;

namespace LittleBlog.Tests
{
    [TestFixture]
    public class TagUtilTest
    {
        private Mock<ITagRepository> _mockTagRepository;
        private TagUtil _tagUtil;
        
        private static readonly Article article = new Article()
        {
            Description = "Lorem ipsum",
            Header = "Lorem",
            Id = 1,
            TimeForRead = 20,
        };

        private static IList<Tag> _fakeDbTags;

        [SetUp]
        public void SetUp()
        {
            _mockTagRepository = new Mock<ITagRepository>();
            _tagUtil = new TagUtil(_mockTagRepository.Object);
            _fakeDbTags = new List<Tag>();
            
            var tags = article.Tags = new List<Tag>();
            tags.Add(new Tag() { Id = 1, Name = "Hello1"} );
            tags.Add(new Tag() { Id = 2, Name = "Hello2"} );
            tags.Add(new Tag() { Id = 3, Name = "Hello3"} );
            tags.Add(new Tag() { Id = 4, Name = "Hello4"} );
            tags.Add(new Tag() { Id = 5, Name = "Hello5"} );
            tags.Add(new Tag() { Id = 6, Name = "Hello6"} );
            
            _mockTagRepository.Setup(s => s.GetAll()).Returns(_fakeDbTags);

        }
        
        [Test]
        public void GetTagsIfDbTagsNotExits_Test()
        {
            var articlesTags = _tagUtil.GetTags(article);
            
            Assert.AreEqual(6, articlesTags.Count);
        }
        
        [Test]
        public void GetTagsIfDbTagExits_Test()
        {
            var temp = new Tag() {Id = 2, Name = "Hello2"};
            _fakeDbTags.Add(temp);
            _fakeDbTags.Add(new Tag() { Id = 7, Name = "Hello7"} );
            _fakeDbTags.Add(new Tag() { Id = 5, Name = "Hello5"} );
            
            ICollection<Tag> articlesTags = _tagUtil.GetTags(article);
            
            Assert.AreEqual(6, articlesTags.Count);
            Assert.Contains(temp, articlesTags.ToList());
        }
    }
}