using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.Dtos.Article;
using LittleBlog.Exceptions;
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
        private readonly IFileService _fileService;

        public ArticlesController(
            IArticleService articleService,
            IAccountService accountService,
            ICommentService commentService,
            IAuthenticationService authenticateService,
            IMapper mapper,
            IFileService fileService,
            ILoggerService loggerService) : base(accountService, mapper, authenticateService, loggerService)
        {
            this._articleService = articleService;
            this._commentService = commentService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("create")]
        public ActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateArticle(CreateArticleViewModel model, IEnumerable<HttpPostedFileBase> files)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            return CreateActionResult(() =>
            {
                var article =  Mapper.Map<CreateArticleViewModel, CreateArticleDTO>(model);

                if (files != null)
                    article.Images = GetImagesNameAndSaveOnServer(files);
                
                this._articleService.AddArticle(article);
                
                TempData["status"] = "Success";
                
                return View();
            });
        }

        [HttpPost]
        [Route("update")]
        public ActionResult UpdateArticle(GetArticleViewModel model)
        {
            return CreateActionResult(() =>
            {
                this._articleService.UpdateArticle(
                    Mapper.Map<GetArticleViewModel, GetArticleDTO>(model));

                return RedirectToAction("PreviewArticles", "Articles");
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

        [Route("file/{name}")]
        public ActionResult GetFileByName(string name)
        {
            return CreateActionResult(() =>
            {
                ImageDTO fileName = this._fileService.GetFileByName(name);

                string fileExt = Path.GetExtension(fileName.ImageUrl)?.Remove(0, 1);
                
                string contentType = "image/" + (fileExt == "jpg" ? "jpeg" : fileExt);
                
                byte[] image = 
                    System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~"), fileName.ImageUrl));
                
                return base.File(image, contentType);
            });
        }

        #region Helpers

        public ICollection<ImageDTO> GetImagesNameAndSaveOnServer(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                string[] ext = {".jpg", ".jpeg", ".png", ".gif"};
                
                LinkedList<ImageDTO> images = new LinkedList<ImageDTO>();
                
                foreach (var file in files)
                {
                   var fileExt = Path.GetExtension(file.FileName);
    
                   if (!ext.Contains(fileExt))
                   {
                       throw FileException.UnpropriateFileExtension(fileExt);
                   }

                   images.AddLast(new ImageDTO() { ImageUrl = Path.GetFileName(file.FileName) });
                   file.SaveAs(Path.Combine(Server.MapPath(@"~/App_Data"), file.FileName));                
                }
                
                return images;
            }
            
            return null;
        }

        #endregion
    }
}