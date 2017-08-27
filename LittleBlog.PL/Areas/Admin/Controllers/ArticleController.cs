using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.Dtos.Article;
using LittleBlog.PL.Controllers;
using LittleBlog.ViewModels.Article;

namespace LittleBlog.PL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("articles")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;

        public ArticlesController(
            IArticleService articleService,
            IAccountService accountService,
            ICommentService commentService,
            IMapper mapper) : base(accountService, mapper)
        {
            this._articleService = articleService;
            this._commentService = commentService;
        }

        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("get-all")]
        public ActionResult PreviewArticles(int count = 0, 
                                            int startWith = 0)
        {
            return CreateActionResult(() =>
            {
                var articlesPreviewDto = this._articleService.ShowPreviewArticle(startWith, count, 20);
                return View(Mapper.Map<IEnumerable<GetArticleDTO>, IEnumerable<GetArticleViewModel>>(articlesPreviewDto));
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
        public ActionResult UpdateArticle(GetArticleViewModel model)
        {
            return CreateActionResult(() =>
            {
                this._articleService.UpdateArticle(
                    Mapper.Map<GetArticleViewModel, GetArticleDTO>(model));

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
                return View(Mapper.Map<GetArticleDTO, GetArticleViewModel>
                    (this._articleService.GetArticleById(id)));
            });
        }

        [HttpPost]
        public ActionResult CreateCommentAjax([Bind(Exclude = "Id,DateTime")]CommentViewModel model, int articleId)
        {
            return CreateActionResult(() =>
            {

                var dto = Mapper.Map<CommentViewModel, CommentDTO>(model);
                this._commentService.CreateCommentByArticleId(dto, articleId);

                return View("GetArticle", (Mapper.Map<GetArticleDTO, GetArticleViewModel>
                    (this._articleService.GetArticleById(articleId))));
            });
        }
    }
}