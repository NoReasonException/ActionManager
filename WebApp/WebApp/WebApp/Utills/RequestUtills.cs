using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Utills
{
    public class RequestUtills
    {
        /// <summary>
        /// Sends a GET Request 
        /// </summary>
        /// <param name="Url">The Target URL</param>
        /// <throws> <see cref="WebException"/> in case of return 404 or 400</throws>
        /// <returns></returns>
        public static  System.String Get(System.String Url)
        {
            HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(Url);
            GetRequest.AutomaticDecompression = DecompressionMethods.GZip; // Turn Automatic Decompress On! ()
            try
            {
                HttpWebResponse ReturnResponce = (HttpWebResponse)GetRequest.GetResponse();
                StreamReader Reader = new StreamReader(ReturnResponce.GetResponseStream());
                return Reader.ReadToEnd();
            }
            catch (Exception)
            {

                return System.String.Empty;
            }
            
        }
        /// <summary>
        /// Sends A post request with application/x-www-form-urlencoded
        /// </summary>
        /// <param name="Url">Target Url</param>
        /// <param name="FormData">Raw Form Data</param>
        /// <returns>True if Server Respond OK(200) , False OtherWise</returns>
        public static bool PostForm(System.String Url,System.String FormData)
        {
            bool status;
            HttpWebRequest PostRequest = (HttpWebRequest)WebRequest.Create(Url);
            byte [] data = Encoding.ASCII.GetBytes(FormData);
            PostRequest.Method = "POST";
            PostRequest.ContentType = "application/x-www-form-urlencoded";
            PostRequest.ContentLength = data.Length;
            using (var stream = PostRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }//in this case , a simple bool is optimal :: , we dont need exceptions and handlers everywhere!
            try
            {
                HttpWebResponse ReturnResponce = (HttpWebResponse)PostRequest.GetResponse();
                status = ReturnResponce.StatusCode == HttpStatusCode.OK;
                Debug.WriteLine("PUT Request returned status " + status);
                return status;

            }
            catch (WebException e) { return false; }
        }
        public static bool PutForm(System.String Url, System.String FormData)
        {
            Debug.WriteLine("PUT Request on {0} with data {1} ", Url,FormData);
            bool status;
            HttpWebRequest PutRequest = (HttpWebRequest)WebRequest.Create(Url);
            byte[] data = Encoding.ASCII.GetBytes(FormData);
            PutRequest.Method = "PUT";
            PutRequest.ContentType = "application/x-www-form-urlencoded";
            PutRequest.ContentLength = data.Length;
            using (var stream = PutRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //in this case , a simple bool is optimal :: , we dont need exceptions and handlers everywhere!
            try
            {
                HttpWebResponse ReturnResponce = (HttpWebResponse)PutRequest.GetResponse();
                status = ReturnResponce.StatusCode == HttpStatusCode.OK;
                Debug.WriteLine("PUT Request returned status " + status);
                return status;

            }
            catch(WebException e) { return false; }
            
        }
    }
}
