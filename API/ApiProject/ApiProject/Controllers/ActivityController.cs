using ApiProject.DBClasses;
using System.Web.Http;
using ApiProject.DBClasses.DB_EFContext;
using System.Web.Http.Results;
using ApiProject.DBClasses;
using ApiProject.DBClasses.DB_EFContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiProject.Controllers
{
    
    public class ActivityController : ApiController
    {
        ///TODO:Code Duplication -> FixIT! One Common Static con and JsonSettings for both Controllers!
        static Context con = new Context();
        static Newtonsoft.Json.JsonSerializerSettings JsonSettings;

        static ActivityController()
        {
            con = new Context();
            JsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            JsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
        [Route("api/Activity/{id}")]
        [HttpGet]
        public JsonResult<Activity[]> Get(int id)
        {
            Debug.WriteLine("api/Activity/id Get Controller: Incoming Request GET_ALL_ACTIVITIES with ID{0} from {1}",id, Request.Headers.From);

            Activity[] ActivitiesFromDB;
            try
            {
                ActivitiesFromDB = con.ActivityContainer.Where<Activity>(o => o.Customer.CustomerID == id).ToArray();
            }catch(Exception e)
            {
                Debug.WriteLine("api/Activity/id Get Controller ,[From :{0}] Encountered a fatal error {1} ",Request.Headers.From,e.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadGateway)
                {
                    ReasonPhrase = "FATAL ERROR"
                });
            }
            Debug.WriteLine("api/Activity/id Get Controller , Handles Appropiate Request , Returns 200! [From {0}]",Request.Headers.From);
            return Json(ActivitiesFromDB,JsonSettings);
        }
    }
}
