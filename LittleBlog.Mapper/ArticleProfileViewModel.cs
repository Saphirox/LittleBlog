using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LittleBlog.Dtos.Article;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Article;
using LittleBlog.ViewModels.Article;
using LittleBlog.ViewModels.Identity;
using LittleBlog.Entities.Identity;

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
            CreateMap<GetArticleViewModel, GetArticleDTO>().ReverseMap();

            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>()
                .ForMember(src => src.DateTime, 
                           opt => opt.MapFrom(
                           src => DateTime.UtcNow));

            CreateMap<CommentViewModel, CommentDTO>().ReverseMap();

            CreateMap<ImageDTO, Image>().ReverseMap();
            CreateMap<ImageDTO, ImageViewModel>().ReverseMap();

            CreateMap<PublishEditDate, PublishEditDateDTO>().ReverseMap();
            CreateMap<PublishEditDateDTO, PublishEditDateViewModel>().ReverseMap();

            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<TagDTO, TagViewModel>().ReverseMap();

            /* Mappers module */
            CreateMap<SignInViewModel, AccountDTO>();
            CreateMap<RegisterViewModel, AccountDTO>();
            
            CreateMap<AccountViewModel, AccountDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
        }
    }
}