using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiProject.DBClasses;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace WebApp.Controllers
{
    public class EditCustomerController : Controller
    {
        public IActionResult Index(string id)
        {
            
            if (Request.Method.Equals("POST"))
            {
                ViewBag.Success = PostActivities();
                
            }
            if (String.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Customer cust = WebApp.Utills.Service.WebServiceUtills.JsonToCustomerDecoder(WebApp.Utills.Service.WebService.GetCustomerByID(System.Int32.Parse(id)));
                ViewBag.cust = cust;
                Debug.WriteLine(cust.CustomerID);
                 
            }catch(WebException e)
            {
                return BadRequest(); //id not found or server down , redirect to error!

                //TODO : Fix it properly
            }
            catch(JsonReaderException e)
            {
                return BadRequest(); //Better return 500? 
            }
            return View();
        }
        public bool PostActivities()
        {
            System.String str;
            try
            {
                bool flag = true;
                for(int i = 0; i < System.Int32.Parse(Request.Form["Results"]); i++)
                {
                   

                    if (!(WebApp.Utills.Service.WebService.UpdateActivity(
                        System.Int32.Parse(Request.Form["ActivityID" + i]), 
                        new ApiProject.DBClasses.Activity(
                            System.Int32.Parse(
                                Request.Form["ActivityID" + i])
                                , null
                                , Request.Form["Description" + i]
                                , new DateTime(2017, 1, 1)
                                , new DateTime(2018, 1, 1)))));
                    {

                        Debug.WriteLine(Request.Form["ActivityID" + i]);
                        Debug.WriteLine(Request.Form["Description" + i]);
                        flag = false;
                    }
                }
                return flag;
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            
        }
    }
}