using LittleBlog.DAL.Repositories;
 
 namespace LittleBlog.DAL.UnitOfWorks
 {
     public interface IArticleUnitOfWork :  IUnitOfWork
     {
           IArticleRepository ArticleRepository { get; set; }
           ITagRepository TagRepository { get; set; }
     }
 }