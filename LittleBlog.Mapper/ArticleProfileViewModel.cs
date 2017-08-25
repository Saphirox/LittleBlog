using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<CreateArticleViewModel, CreateArticleDTO>()
                .ForMember(src => src.Tags, 
                    opt => opt.MapFrom(src => src.Tags.Split(' ')
                        .Select(x => new TagDTO { Name = x }).ToList()));

            CreateMap<CreateArticleDTO, Article>()
                .ForMember(src => src.PublishEditDates,
                    opt => opt.MapFrom(src => new List<PublishEditDateDTO>
                        {new PublishEditDateDTO {Date = DateTime.UtcNow}}));
            
            CreateMap<Article, GetArticleDTO>().ReverseMap();
            CreateMap<ArticleViewModel, GetArticleDTO>().ReverseMap();

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