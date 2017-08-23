using System;
using LittleBlog.Dtos.Shared;

namespace LittleBlog.Dtos.Article
{
    public class PublishEditDateDTO : DTO
    {
        public DateTime Date { get; set; }
    }
}