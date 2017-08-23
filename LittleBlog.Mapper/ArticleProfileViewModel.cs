using AutoMapper;
using LittleBlog.Dtos.Article;
using LittleBlog.Entities.Article;
using LittleBlog.ViewModels.Article;

namespace LittleBlog.Mapper
{
    public class ArticleProfileViewModel : Profile
    {
        public ArticleProfileViewModel()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<ArticleViewModel, ArticleDTO>().ReverseMap();

            CreateMap<CommentDTO, Comment>().ReverseMap();
            CreateMap<CommentDTO, CommentViewModel>().ReverseMap();

            CreateMap<ImageDTO, Image>().ReverseMap();
            CreateMap<ImageDTO, ImageViewModel>().ReverseMap();

            CreateMap<PublishEditDate, PublishEditDateDTO>().ReverseMap();
            CreateMap<PublishEditDateDTO, PublishEditDateViewModel>().ReverseMap();

            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<TagDTO, TagViewModel>().ReverseMap();
        }
    }
}