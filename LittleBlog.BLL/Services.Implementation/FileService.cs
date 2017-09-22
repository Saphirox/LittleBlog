using System.IO;
using System.Linq;
using AutoMapper;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using LittleBlog.DAL.UnitOfWorks;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using LittleBlog.Exceptions;

namespace LittleBlog.BLL.Services.Implementation
{
    public class FileService : Service<IArticleUnitOfWork>, IFileService
    {
        public FileService(IArticleUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {}
        
        public ImageDTO GetImageByName(string imageName)
        {
            string[] ext = {".jpg", ".jpeg", ".png", ".gif"};

            ImageDTO image = Mapper.Map<Image, ImageDTO>(
                UnitOfWork.ArticleRepository.GetAll()
                    .SelectMany(a => a.Images).FirstOrDefault(i => 
                        Path.GetFileNameWithoutExtension(i.ImageUrl) == imageName)
            );
            
            if (image == null)
                throw FileException.FileNameNotExists(imageName);

            return image;
        }
    }
}