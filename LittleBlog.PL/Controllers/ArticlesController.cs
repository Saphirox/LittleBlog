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
    [AllowAnonymous]
    [RoutePrefix("articles")]
    public class ArticlesController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IViewArticleService _viewArticleService;

        public ArticlesController(
            IViewArticleService viewArticleService,
            IAccountService accountService,
            IAuthenticationService authenticateService,
            ICommentService commentService,
            ILoggerService loggerService,
            IMapper mapper) : base(accountService, mapper, authenticateService, loggerService)
        {
            _viewArticleService = viewArticleService;
            _commentService = commentService;
        }


        /// <summary>
        /// Get headers of article and it preview text
        /// </summary>
        /// <param name="count"></param>
        /// <param name="startWith"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all")]
        public ActionResult PreviewArticles(int count = 0, int startWith=0)
        {
            return CreateActionResult(() =>
            {
                var articlesPreviewDto = this._viewArticleService.GetPreviewArticles(startWith, count, 20);

                var viewModelsArticles = Mapper.Map<IEnumerable<GetArticleDTO>, IEnumerable<GetArticleViewModel>>(articlesPreviewDto);

                return View(new PreviewArticlesViewModel(viewModelsArticles, this._viewArticleService.CountArticles()));
            });
        }

        /// <summary>
        /// Get article by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetArticle(int id)
        {
            return CreateActionResult(() =>
            {
                return View(Mapper.Map<GetArticleDTO, GetArticleViewModel>
                    (this._viewArticleService.GetArticleById(id)));
            });
        }

        /// <summary>
        /// Comment article using asynchronous 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-comment")]
        public ActionResult CreateCommentAjax(CommentViewModel model, int articleId)
        {
            return CreateActionResult(() =>
            {

                var dto = Mapper.Map<CommentViewModel, CommentDTO>(model);
                this._commentService.CreateCommentByArticleId(dto, articleId);

                return RedirectToAction("GetArticle", (Mapper.Map<GetArticleDTO, GetArticleViewModel>
                    (this._viewArticleService.GetArticleById(articleId))));
            });
        }
    }
}