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
        private readonly ICommentService _commentService;

        public ArticlesController(
            IArticleService articleService, 
            IMapper mapper) : base(mapper)
        {
            this._articleService = articleService;
        }
        
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("get-all")]
        public ActionResult PreviewArticles(int count=0, int startWith=0)
        {
            return CreateActionResult(() =>
            {
                var articlesPreviewDto = this._articleService.ShowPreviewArticle(startWith, count);
                return View(Mapper.Map<IEnumerable<GetArticleDTO>, IEnumerable<ArticleViewModel>>(articlesPreviewDto));
            });
        }

        [HttpGet]
        [Route("create")]
        public ActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateArticle(CreateArticleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            return CreateActionResult(() =>
            {
                this._articleService.AddArticle(
                    Mapper.Map<CreateArticleViewModel, CreateArticleDTO>(model));
                TempData["status"] = "Success";
                return View();
            });
        }

        [HttpPost]
        [Route("create/update")]
        public ActionResult UpdateArticle(ArticleViewModel model)
        {
            return CreateActionResult(() =>
            {
                this._articleService.UpdateArticle(
                    Mapper.Map<ArticleViewModel, GetArticleDTO>(model));

                return RedirectToAction("Index");
            });
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult DeleteArticle(int id)
        {
            return CreateActionResult(() =>
            {
                this._articleService.DeleteArticle(id);
                return RedirectToAction("PreviewArticles");
            });
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetArticle(int id)
        {
            return CreateActionResult(() =>
            {
                return View(Mapper.Map<GetArticleDTO, ArticleViewModel>
                    (this._articleService.GetArticleById(id)));
            });
        }

        [HttpPost]
        public ActionResult CreateCommentAjax(CommentViewModel model, int articleId)
        {
            return CreateActionResult(() =>
            {
                this._commentService.CreateCommentByArticleId(
                    Mapper.Map<CommentViewModel, CommentDTO>(model), articleId
                    );

                return View("GetArticle", (Mapper.Map<GetArticleDTO, ArticleViewModel>
                    (this._articleService.GetArticleById(articleId))));
            });
        }
    }
}