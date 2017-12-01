using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class NewActivityController : Controller
    {

        public IActionResult Index(System.String id)
        {
            if (String.IsNullOrEmpty(id)) {
                return BadRequest();
                
            }
            if (this.Request.Method.Equals("POST")) {
                ViewBag.Success = this.PostActivity(Int32.Parse(id));
            }
            return View();
        }
        [NonAction]
        public bool PostActivity(System.Int32 CustomerID)
        {
            return WebApp.Utills.Service.WebService.PostActivity(CustomerID, new ApiProject.DBClasses.Activity(
                0,
                null,
                Request.Form["Description"],
                WebApp.Utills.MiscUtills.ConvertFromHtmlDateTimeFormat(Request.Form["StartDate"]),
               WebApp.Utills.MiscUtills.ConvertFromHtmlDateTimeFormat(Request.Form["EndDate"])));
            
        }
    }
}