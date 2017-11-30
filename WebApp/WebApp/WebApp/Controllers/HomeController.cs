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

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        static HomeController()
        {
            Utills.Service.WebService.ServiceUrl = "http://localhost:59644";
        }
        public IActionResult Index()
        {
            System.String GetAll = Utills.Service.WebService.GetAllRecords();
            List<List<System.String>> ToIndex = new List<List<System.String>>();
            List<System.String> Temp;
            List<Object> Data = JsonConvert.DeserializeObject<List<Object>>(GetAll);
            foreach(JObject Datum in Data)
            {
                Temp = new List<System.String>();


                Temp.Add(Datum["CustomerID"].ToString());
                Temp.Add(Datum["Name"].ToString());
                Temp.Add(Datum["Address"].ToString());
                ToIndex.Add(Temp);
            }

            ViewBag.Str = ToIndex;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
