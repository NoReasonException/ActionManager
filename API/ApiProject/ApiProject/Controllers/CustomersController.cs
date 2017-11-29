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
    public class CustomersController : ApiController
    {

        static Context con = new Context();
        static Newtonsoft.Json.JsonSerializerSettings JsonSettings;

        static CustomersController()
        {
            con = new Context();
            JsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            JsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }

        /// <summary> The Main GET Method , Returns All the Customers.</summary>
        /// <remarks> The Main GET Method , Its gonna used by the main grid , 
        ///     NOTE: This call returns only the Customer Object , with no Activities Asciosiated with it , 
        ///     To get the activities , pleace call the GetByID.
        ///     </remarks>
        /// <see cref="ApiProject.DBClasses.Customer"/>
        /// <see cref="ApiProject.DBClasses.DB_EFContext.Context"/>
        /// <returns>A Json Result</returns>
        [HttpGet]
        public JsonResult<Customer[]> Get()
        {

            Customer[] CustomersArray = con.CustomerContainer.ToList().ToArray();
            try
            {
                foreach (Customer temp in CustomersArray)
                {
                    temp.Activities = con.ActivityContainer.Where(o => o.Customer.CustomerID == temp.CustomerID).ToList();
                    Debug.WriteLine("--->>>" + temp.Name);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "NOT FOUND"

                });

            }
            JsonResult<Customer[]> retval;
            try
            {
                retval = this.Json(CustomersArray, JsonSettings);

            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadGateway)
                {
                    ReasonPhrase = "SERIALIZATION METHOD FAIL"

                });
            }
            return retval;
        }






    }
}
