using System.Collections.Generic;
using System.Linq;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Entities.Article;

namespace LittleBlog.BLL.Infrastructure
{
    public class TagUtil
    {
        private readonly ITagRepository _tagRepository;

        public TagUtil(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        
        public ICollection<Tag> GetTags(Article entity)
        {
            var listOfTags = new List<Tag>();
            
            var dbTags = this._tagRepository.GetAll().ToList();
            
            var entityTags = entity.Tags;

            if (!dbTags.Any())
            {
                listOfTags.AddRange(entityTags);
            }
            else
            {
                listOfTags = listOfTags.Concat(dbTags.Where(d => entityTags.Any(e => e.Name == d.Name))).ToList();
                listOfTags = listOfTags.Concat(entityTags.Where(d => listOfTags.All(e => e.Name != d.Name))).ToList();
            }
            
            return listOfTags;
        }
    }
}