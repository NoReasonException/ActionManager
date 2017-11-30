using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utills;
namespace WebApp.Utills.Service
{
    public class WebService
    {
        static System.String ServiceUrl;
        static WebService()
        {
            ServiceUrl = System.String.Empty;
            
        }
        

        public static System.String GetAllRecords(System.String ServiceLocation)
        {
            return WebApp.Utills.RequestUtills.Get(ServiceLocation + "/api/Customers");

        }
        private static void Check()
        {
            if (ServiceUrl.Equals(System.String.Empty)) throw new InvalidOperationException("Set the WebService.ServiceUrl First!");
            
        }
        public static System.String GetAllRecords() 
        {
            Check();
            return WebApp.Utills.RequestUtills.Get(WebService.ServiceUrl + "/api/Customers");
        }
}
