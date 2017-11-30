using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApp.Utills
{
    public class RequestUtills
    {
        /// <summary>
        /// Sends a GET Request 
        /// </summary>
        /// <param name="Url">The Target URL</param>
        /// <returns></returns>
        public static  System.String Get(System.String Url)
        {
            HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(Url);
            GetRequest.AutomaticDecompression = DecompressionMethods.GZip; // Turn Automatic Decompress On! ()

            HttpWebResponse ReturnResponce = (HttpWebResponse)GetRequest.GetResponse();
            StreamReader Reader = new StreamReader(ReturnResponce.GetResponseStream());
            return Reader.ReadToEnd();
        }
    }
}
