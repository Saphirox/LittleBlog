using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Identity;
using LittleBlog.ViewModels.Identity;
using Microsoft.AspNet.Identity;

namespace LittleBlog.PL.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IAccountService AccountService;
        protected readonly IMapper Mapper;
        protected readonly IAuthenticateService AuthenticateService;

        public BaseController(IAuthenticateService authenticateService)
        {
            AuthenticateService = authenticateService;
        }

        public BaseController(
            IAccountService accountService, 
            IMapper mapper, 
            IAuthenticateService authenticateService)
        {
            AccountService = accountService;
            Mapper = mapper;
            AuthenticateService = authenticateService;
        }
        
        /// <summary>
        /// Get current user using email
        /// </summary>
        public AccountViewModel CurrentUser 
        { 
            get {
                var userDto =  
                    AccountService.GetUserByName(User.Identity.GetUserName());
    
                return Mapper.Map<AccountDTO, AccountViewModel>(userDto);
            }
        }
        
        /// <summary>
        ///  ThrowIf pattern using for catching all errors
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult CreateActionResult(Func<ActionResult> request)
        {
            ActionResult result = null;

            try
            {
                result = request();
            }
            catch (DbUpdateException ex)
            {
                result = View("Error", ex.Message);
            }
            catch (Exception ex)
            {
                result = View("Error", ex.Message);
            }

            return result;
        }
        
        /// <summary>
        /// asynchronous CreateActionResult 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ActionResult> CreateActionResultAsync(Func<Task<ActionResult>> request)
        {
            ActionResult result = null;

            try
            {
                result = await request();
            }
            catch (DbUpdateException ex)
            {
                result = View("Error", ex.Message);
            }
            catch (Exception ex)
            {
                result = View("Error", ex.Message);
            }

            return result;
        }
    }
}