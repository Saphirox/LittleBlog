using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.Tests
{
    public static class FakeArticles
    {
        private static int publishEditDate = 1;

        public static CreateArticleDTO CreateArticleWithTwoTagsDto()
           => new CreateArticleDTO()
           {
               Id = 2,
               Description = "Lorem ipsum else some count else description porko rosso",
               Header = "Lorem",
               Tags = CreateTagsDto()
           };

        public static GetArticleDTO CreateGetArticle()
            => new GetArticleDTO
            {
                Id = 2,
                Description = "Lorem ipsum else some count else description porko rosso",
                Header = "Lorem",
                Tags = CreateTagsDto()
            };

        public static Article CreateArticle()
        => new Article
        {

            Id = 2,
            Description = "Lorem ipsum else some count else description porko rosso",
            Header = "Lorem",
        };

        public static IList<TagDTO> CreateTagsDto()
            => new[] { new TagDTO { Name = "FirstTag" }, new TagDTO { Name = "SecondTag" } };

        public static IList<Tag> CreateTags()
           => new[] { new Tag { Name = "FirstTag" }, new Tag { Name = "SecondTag" } };

        public static Article CreateArticleWithOneTagAndOneDate()
            => new Article
            {
                Description = "Lorem ipsum else some count",
                Header = "Lorem",
                Id = 1,
                TimeForRead = 20,
                Tags = new[] { new Tag() { Name = "Hello" } },
                PublishEditDates = CreatePublishEditDates(null)
            };

        public static IList<PublishEditDate> CreatePublishEditDates(Article article)
           => new[] { new PublishEditDate() { Article = article, Id = publishEditDate++, Date = DateTime.UtcNow } };
    }
}
