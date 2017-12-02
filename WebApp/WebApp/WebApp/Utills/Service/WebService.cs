using ApiProject.DBClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Utills;
namespace WebApp.Utills.Service
{

    /// <summary>
    /// This Class Is A Wrapper Over WebAPI Project , To Retrieve Easily Information 
    /// uses the <see cref="Utills.RequestUtills.RequestUtills"/>
    /// 
    /// </summary>
    public class WebService
    {
        /// <summary>
        /// Remember! Always Set <see cref="WebService.ServiceUrl"/> Attritude before use , OtherWise <see cref="InvalidOperationException"/> will thrown by every call!
        /// </summary>
        public static System.String ServiceUrl;
        static WebService()
        {
            ServiceUrl = System.String.Empty;
            
        }
        /// <summary>
        /// Checks if <see cref="WebService.ServiceUrl"/> Attritide is Set! throws <see cref="InvalidOperationException"/> otherwise
        /// </summary>
        private static void Check()
        {
            if (ServiceUrl.Equals(System.String.Empty)) throw new InvalidOperationException("Set the WebService.ServiceUrl First!");
            
        }
        /// <summary>
        /// Get all records using the Rest WebApi 
        /// Calls GET <urlOfService>/api/Customers 
        /// 
        /// </summary>
        /// <returns> The Responce as  <see cref="System.String"/> Object</returns>
        public static System.String GetAllCustomers()
        {
            Check();
            return WebApp.Utills.RequestUtills.Get(WebService.ServiceUrl + "/api/Customers");
        }
        /// <summary>
        /// Get Record by ID 
        /// Calls GET <urlOfService>/api/Customers/{Id}
        /// </summary>
        /// <param name="Id"> The ID of customer</param>
        /// <returns> The Responce as  <see cref="System.String"/> Object</returns>

        public static System.String GetCustomerByID(int Id)
        {
            Check();
            return WebApp.Utills.RequestUtills.Get(WebService.ServiceUrl + "/api/Customers/" + Id.ToString());

        }
        /// <summary>
        /// 
        /// Submit A new Customer to Database , Using Web Api!
        /// uses the <see cref="WebApp.Utills.RequestUtills.PostForm(string, string)"/> Utillity
        /// </summary>
        /// <param name="FormData"></param>
        /// <returns>True if everything is fine (Server Responce Ok(200) ) False OtherWise</returns>
        public static bool PostNewCustomer(System.Collections.Generic.Dictionary<System.String, System.String> FormData)
        {
            Check();
            return WebApp.Utills.RequestUtills.PostForm(ServiceUrl+ "/api/Customers", WebApp.Utills.Service.WebServiceUtills.SubmitNewCustomer_FormEncoder(FormData));
        }

        public static bool UpdateActivity(int id, ApiProject.DBClasses.Activity act)
        {
            Check();
            Debug.WriteLine("UpdateActivity Invoked on ActivityID " + act.ActivityID);

            bool status = WebApp.Utills.RequestUtills.PutForm(ServiceUrl + "/api/Activity/" + id,
                 WebApp.Utills.Service.WebServiceUtills.UpdateActivity_FormEncoder(new Dictionary<string, string>()
                 {
                     { "ActivityID" ,act.ActivityID.ToString()},
                     { "Description" ,act.Description},
                     { "StartDate" ,act.StartDate.ToString("yyyy-MM-dd HH:mm:ss")},
                     { "EndDate" ,act.EndDate.ToString("yyyy-MM-dd HH:mm:ss")},
                 }));;
            Debug.WriteLine("UpdateActivity Invoked on ActivityID {0} (returned status {1}) " , act.ActivityID,status);

            return status;
        }
        public static bool PostActivity(int customerID,ApiProject.DBClasses.Activity act)
        {
            Check();
            Debug.WriteLine("PostActivity Invoked on CustomerID " + customerID);

            bool status = WebApp.Utills.RequestUtills.PostForm(ServiceUrl + "/api/Activity/" + customerID,
                 WebApp.Utills.Service.WebServiceUtills.PostActivity_FormEncoder(new Dictionary<string, string>()
                 {
                     { "Description" ,act.Description},
                     { "StartDate" ,act.StartDate.ToString("yyyy-MM-dd HH:mm:ss")},
                     { "EndDate" ,act.EndDate.ToString("yyyy-MM-dd HH:mm:ss")},
                 })); ;
            Debug.WriteLine("PostActivity Invoked on CustomerID {0} (returned status {1}) ",customerID, status);

            return status;
        }
    }
}
