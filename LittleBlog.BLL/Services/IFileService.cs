using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IFileService
    {
        ImageDTO GetImageByName(string name);
    }
}