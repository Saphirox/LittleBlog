using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace LittleBlog.PL.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

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
    }
}