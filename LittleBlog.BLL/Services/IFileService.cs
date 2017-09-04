using LittleBlog.Dtos.Article;

namespace LittleBlog.BLL.Services
{
    public interface IFileService
    {
        ImageDTO GetFileByName(string name);
    }
}