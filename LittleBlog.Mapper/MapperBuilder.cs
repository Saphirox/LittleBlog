using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace LittleBlog.Mapper
{
    public class MapperBuilder
    {
        public static IMapper BuildMapper()
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile(new ArticleProfileDTO());
                cfg.AddProfile(new ArticleProfileViewModel());
            });

            //config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
