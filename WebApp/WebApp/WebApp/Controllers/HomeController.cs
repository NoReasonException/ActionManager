using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WebApp.Controllers
{
    //todo , when api not respond , redirect to error..
    public class HomeController : Controller
    {
        static HomeController()
        {
            Utills.Service.WebService.ServiceUrl = "http://localhost:59644";
        }
        public IActionResult Index()
        {
            ///TODO:Remove that , make utillity instead! />
            try
            {

                System.String GetAll = Utills.Service.WebService.GetAllCustomers();
                List<List<System.String>> ToIndex = new List<List<System.String>>();
                List<System.String> Temp;
                List<Object> Data = JsonConvert.DeserializeObject<List<Object>>(GetAll);
                foreach (JObject Datum in Data)
                {
                    Temp = new List<System.String>();


                    Temp.Add(Datum["CustomerID"].ToString());
                    Temp.Add(Datum["Name"].ToString());
                    Temp.Add(Datum["Address"].ToString());
                    ToIndex.Add(Temp);
                }

                ViewBag.Str = ToIndex;
                return View();


            }catch(WebException e)
            {
                return BadRequest(); //Server is down!//todo , fixit
            }

        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
