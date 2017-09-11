using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.Dtos.Identity;
using LittleBlog.Exceptions;
using LittleBlog.ViewModels.Identity;
using Microsoft.AspNet.Identity;

namespace LittleBlog.PL.Controllers
{
    [RoutePrefix("identity")]
    public class AccountController : BaseController
    {
        public AccountController(
            IAccountService accountService,
            IMapper mapper,
            IAuthenticateService authenticateService, 
            ILoggerService loggerService) 
            : base(accountService, mapper, authenticateService, loggerService)
        {}
        
        [HttpGet]
        [Route("signin")]
        public ActionResult SignIn(string returnUrl)
        {
            var model = new SignInViewModel()
            {
                ReturnUrl = returnUrl
            };
            
            return View(model);
        }

        [HttpGet]
        [Route("signup")]
        public ActionResult Register()
        {
            return View();
        } 
        
        /// <summary>
        /// Authorize user using claims
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signin")]
        public Task<ActionResult> SignIn(SignInViewModel viewModel)
        {
            return CreateActionResultAsync(async () =>
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                var dtoAccount = 
                    Mapper.Map<SignInViewModel, AccountDTO>(viewModel);
                
                ClaimsIdentity claims;
                
                try
                {
                    claims = await AuthenticateService.Authenticate(dtoAccount);
                }
                catch (IdentityException e)
                {
                    return Content(e.Message);
                }

                SignIn(claims);
                
                return RedirectToAction("Index", "Home");
            });
        }

      
        
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            return CreateActionResultAsync(async () =>
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                var dtoAccount = 
                    Mapper.Map<RegisterViewModel, AccountDTO>(viewModel); 
                
                AccountService.Create(dtoAccount);
                
                var claims = await AuthenticateService.Authenticate(dtoAccount);

                SignIn(claims);
                
                return RedirectToAction("Index", "Home");
            });
        }
        
        /// <summary>
        /// Sign out system. Be carfull, working with application cookie
        /// TODO: create signout for External Cookie
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("SignIn", "Account");
        }
        
        #region Helpers
        
        public void SignIn(ClaimsIdentity claims)
        {
             Request.GetOwinContext().Authentication.SignIn(claims);
         } 
 
         public string RedirectIfReturnUrlExits(string returnUrl)
         {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
               {
                   return Url.Action("Index", "Home");
               }
           
               return returnUrl;
         }
        
         #endregion
     }
 }