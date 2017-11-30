using ApiProject.DBClasses;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ApiProject.DBClasses.DB_EFContext;
namespace ApiProject.Controllers
{
    public class CustomersController : ApiController
    {
        /// <summary> The Main GET Method , Returns All the Customers.</summary>
        /// <remarks> The Main GET Method , Its gonna used by the main grid , 
        ///     NOTE: This call returns only the Customer Object , with no Activities Asciosiated with it , 
        ///     To get the activities , pleace call the GetByID.
        ///     </remarks>
        /// <see cref="ApiProject.DBClasses.Customer"/>
        /// <see cref="ApiProject.DBClasses.DB_EFContext.Context"/>
        /// <returns>A Json Result</returns>
        [HttpGet]
        [Route("api/Customers/")]
        public JsonResult<Customer[]> Get()
        {

            Customer[] CustomersArray = DatabaseContextSiglenton.Context.CustomerContainer.ToList().ToArray();
            try
            {
                foreach (Customer temp in CustomersArray)
                {
                    temp.Activities = DatabaseContextSiglenton.Context.ActivityContainer.Where(o => o.Customer.CustomerID == temp.CustomerID).ToList();
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
                retval = this.Json(CustomersArray, DatabaseContextSiglenton.JsonSettings);

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

        /// <summary>
        /// 
        /// Utillity Method for obtain the Activities of eatch Customer ,
        /// </summary>
        /// <param name="id"> The ID of the Customer</param>
        /// <returns>A List with Activities Ascociated with the ID</returns>
        /// TODO:Code Duplication , Transfer this to ActivityController
        [NonAction]
        public System.Collections.Generic.List<Activity> getActivitiesByID(int id)
        {
            if (DatabaseContextSiglenton.Context.CustomerContainer.Find(id) == null) return null;
            return DatabaseContextSiglenton.Context.ActivityContainer.Where(o => o.Customer.CustomerID == id).ToList<Activity>();
        }
        /// <summary>
        /// GetByID Method , Use this for obtain all information Ascociated with an Cuctomer!
        /// </summary>
        /// <param name="id">The ID of the Customer</param>
        /// <returns>Well...Everything as JSON !</returns>
        [HttpGet]
        [Route("api/Customers/{id}")]
        public JsonResult<Customer> GetByID(int id)
        {
            try{
                Customer retval = DatabaseContextSiglenton.Context.CustomerContainer.Find(id);
                if (retval == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        ReasonPhrase = "NOT FOUND"

                    });
                }
                retval.Activities = this.getActivitiesByID(id);
                return this.Json(retval, DatabaseContextSiglenton.JsonSettings);

            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadGateway)
                {
                    ReasonPhrase = "Database Consistency is violated , Contact with your sysadmin for further instructions... "

                });
            }

        }
        /// <summary>Creates New Customer , Takes Data via Form</summary>
        /// <param name="cust"> The <see cref="ApiProject.DBClasses.Customer"/> Object to add in the database!</param>
        /// <returns>200 If everything is Okay ,400 if critical data is missing</returns>
        [HttpPost]
        [Route("api/Customers")]
        public IHttpActionResult PostCustomer(Customer cust)
        {
            if (!ModelState.IsValid||cust==null)
            {
                return BadRequest();
            }
            DatabaseContextSiglenton.Context.CustomerContainer.Add(cust);
            try
            {
                DatabaseContextSiglenton.Context.SaveChanges();

            }catch(Exception e)//TODO:Catch Specific Exceptions...
            {
                return BadRequest();
            }
            Debug.WriteLine("Object {0} Injected into DB ", cust);
            return Ok();
        }
        /// <summary>Override the Properties of an <see cref="ApiProject.DBClasses.Customer"/> Object !</summary>
        /// <param name="id"></param>
        /// <param name="cust"></param>
        /// <returns>
        ///     <list type="bullet">
        ///         <item>
        ///             <term>200(OK)</term>
        ///             <description>Everything fine!</description>
        ///         </item>
        ///         <item>
        ///             <term>404(Not Found)</term>
        ///             <description>id not found</description>
        ///         </item>
        ///         <item>
        ///             <term>400(Bad GateWay)</term>
        ///             <description>
        ///                 <list type="bullet">
        ///                     <item>
        ///                      <term>Fatal Error</term>
        ///                     </item>
        ///                     <item>
        ///                      <term>Different ID(Not Allowed)</term>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///     </list>
        /// 
        /// 
        /// </returns>
        [HttpPut]
        [Route("api/Customers/{id}")]
        public IHttpActionResult PutCustomer(int id,Customer cust)
        {
            Customer CustomerFromDB = DatabaseContextSiglenton.Context.CustomerContainer.Find(id);
            if (!ModelState.IsValid || cust == null)    return BadRequest();
            if (CustomerFromDB == null)                 return NotFound();
            if (cust.CustomerID != id)                  return BadRequest();
            Debug.WriteLine("Before: "+CustomerFromDB);
            if (cust.Address != null)
            {
                CustomerFromDB.Address = cust.Address;
            }
            if (cust.Name != null){
                CustomerFromDB.Name = cust.Name;
            }
            Debug.WriteLine("After: "+CustomerFromDB);
            DatabaseContextSiglenton.Context.SaveChanges(); //TODO : try/catch
            return Ok();
        }
    }
}
