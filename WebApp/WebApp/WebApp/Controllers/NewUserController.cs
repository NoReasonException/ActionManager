﻿using System;
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
        /// ///TODO : Support Unicode
        /// </summary>
        /// <returns></returns>
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
        public bool CheckPostRequest()
        {
            if (String.IsNullOrEmpty(Request.Form["Name"])) return false;
            if (String.IsNullOrEmpty(Request.Form["Address"])) return false;
            return true;
        }
    }
}