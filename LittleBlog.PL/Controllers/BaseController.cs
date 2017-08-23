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
        protected IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult CreateActionResult(Func<ActionResult> result)
        {
            try
            {
                return result();
            }
            catch (DbUpdateException ex)
            {
                var error = ex.InnerException;
            }
            catch (Exception e)
            {
                // TODO: hard code
                var error = e.InnerException;
            }

            return result();
        }
    }
}