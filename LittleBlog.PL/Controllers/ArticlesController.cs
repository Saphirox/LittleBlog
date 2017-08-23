using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.Dtos.Article;
using LittleBlog.ViewModels.Article;

namespace LittleBlog.PL.Controllers
{
    [RoutePrefix("articles")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService, IMapper mapper) : base(mapper)
        {
            this._articleService = articleService;
        }
        
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("getall")]
        public ActionResult PreviewArticles(int count, int startWith)
        {
            return CreateActionResult(() =>
            {
                var articlesPreviewDto = this._articleService.ShowPreviewArticle(startWith, count);
                return View(_mapper.Map<IEnumerable<ArticleDTO>, IEnumerable<ArticleViewModel>>(articlesPreviewDto));
            });
        }

        public ActionResult CreateArticle(ArticleViewModel model)
        {
            return CreateActionResult(() =>
            {
                this._articleService.AddArticle(
                    _mapper.Map < IEnumerable < ArticleViewModel >,IEnumerable<ArticleDTO> > (model));
                return RedirectToAction("Index");
            });
        }

        [Route("delete/{id:int}")]
        public ActionResult DeleteArticle(int id)
        {
            return CreateActionResult(() =>
            {
                this._articleService.DeleteArticle();
                return RedirectToAction("Index");
            });
        }
        
    }
}