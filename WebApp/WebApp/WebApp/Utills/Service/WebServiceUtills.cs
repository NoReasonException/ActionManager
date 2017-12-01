﻿using ApiProject.DBClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Utills.Service
{
    public class WebServiceUtills
    {
        /// <summary>
        /// Converts a human readable Dictionary to form data to pass as 2nd parameter in <see cref="Utills.RequestUtills.PostForm(string, string)"/>
        /// </summary>
        /// <param name="FormData">The Dictionary (with "Name" and "Address" keys included)</param>
        /// <returns>a <see cref="System.String"/> , the Html Encoded Form Data!</returns>
        public static System.String SubmitNewCustomer_FormEncoder(System.Collections.Generic.Dictionary<System.String, System.String> FormData)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("Name=");
                builder.Append(FormData["Name"]);
                builder.Append("&");
                builder.Append("Address=");
                builder.Append(FormData["Address"]);
                return builder.ToString();
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("SubmitNewCustomer_FormEncoder encountered an error -> KeyNot Found , ITS A BUUUUGGG!!! you check the existance in NewUserController.cs:PostNew()");
                Console.WriteLine("Read Carefully! :" + e.Message);
                throw new InvalidOperationException("Caused By undefined key! Message->:" + e.Message, e);
            }
        }


        public static ApiProject.DBClasses.Activity JTokenToCustomerDecoder(JToken JTokenData)
        {
            return new ApiProject.DBClasses.Activity();
        }
        public static ApiProject.DBClasses.Customer JsonToCustomerDecoder(System.String JsonString)
        {


            Customer convertedObject = new Customer();

            try
            {
                JObject Data = (JObject)JsonConvert.DeserializeObject<Object>(JsonString);
                convertedObject.CustomerID = System.Int32.Parse((System.String)Data["CustomerID"]);
                convertedObject.Name = (System.String)Data["Name"];
                convertedObject.Address = (System.String)Data["Address"];
                IEnumerable<JToken> bg = Data["Activities"].Children();
                foreach(JToken act in bg)
                {
                    Debug.WriteLine(act["ActivityID"]);
                }
                //convertedObject.Activities = (List<ApiProject.DBClasses.Activity>)bg;
                Debug.WriteLine(convertedObject.CustomerID);
                return convertedObject;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Err!!!");
            }
            
            return new ApiProject.DBClasses.Customer()
            {
                CustomerID = 0,
                Address = "NULL",
                Name = "NULL",
                Activities = new List<ApiProject.DBClasses.Activity>() {
                     new ApiProject.DBClasses.Activity(){
                         ActivityID=0,
                         Customer=null,
                         Description="TestActivity",
                         StartDate=new System.DateTime(2017,1,1),
                         EndDate=new System.DateTime(2017,1,1)
                     },
                }
            };
        }
    }
}
