using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class NewUserController : Controller
    {
        public IActionResult Index()
        {

            if (Request.Method == "POST")
            {
                ViewBag.Success = PostNew();
            }
            return View();
        }
        [NonAction]
        public bool PostNew()
        {
            return true;
        }
        public bool CheckPostRequest()
        {

        }
    }
}