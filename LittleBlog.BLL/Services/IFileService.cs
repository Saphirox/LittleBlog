using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IImageService
    {
        ImageDTO GetFileByName(string name);
    }
}