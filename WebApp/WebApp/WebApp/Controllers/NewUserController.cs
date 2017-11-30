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
        /// <summary>
        /// Handles Self-Post Request to submit new Customer!!
        /// ///TODO : Support Unicode
        /// </summary>
        /// <returns>True if Web Service Respond OK(200) , False OtherWise</returns>
        [NonAction]
        public bool PostNew()
        {
            if (!CheckPostRequest()) return false;
            if(!(WebApp.Utills.Service.WebService.PostNewCustomer(new Dictionary<string, string> {
                {"Name",    Request.Form["Name"] },
                {"Address", Request.Form["Address"] }
            }))){return false;}
            return true;
        }
        /// <summary>
        /// Checks the Post Request for nesessary Data
        /// </summary>
        /// <returns>true if the Post Request is valid , False OtherWise</returns>
        public bool CheckPostRequest()
        {
            if (String.IsNullOrEmpty(Request.Form["Name"])) return false;
            if (String.IsNullOrEmpty(Request.Form["Address"])) return false;
            return true;
        }
    }
}