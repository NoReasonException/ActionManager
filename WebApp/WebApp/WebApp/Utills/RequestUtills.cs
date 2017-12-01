using System;
using System.Collections.Generic;
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
        /// <throws> <see cref="Utills.Service.Errors.NotFoundException"/> in case of return 404 or 400</throws>
        /// <returns></returns>
        public static  System.String Get(System.String Url)
        {
            HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(Url);
            GetRequest.AutomaticDecompression = DecompressionMethods.GZip; // Turn Automatic Decompress On! ()

            HttpWebResponse ReturnResponce = (HttpWebResponse)GetRequest.GetResponse();
            if (!ReturnResponce.StatusCode.Equals(HttpStatusCode.OK)) throw new Utills.Service.Errors.NotFoundException();
            StreamReader Reader = new StreamReader(ReturnResponce.GetResponseStream());
            return Reader.ReadToEnd();
        }
        /// <summary>
        /// Sends A post request with application/x-www-form-urlencoded
        /// </summary>
        /// <param name="Url">Target Url</param>
        /// <param name="FormData">Raw Form Data</param>
        /// <returns>True if Server Respond OK(200) , False OtherWise</returns>
        public static bool PostForm(System.String Url,System.String FormData)
        {
            HttpWebRequest PostRequest = (HttpWebRequest)WebRequest.Create(Url);
            byte [] data = Encoding.ASCII.GetBytes(FormData);
            PostRequest.Method = "POST";
            PostRequest.ContentType = "application/x-www-form-urlencoded";
            PostRequest.ContentLength = data.Length;
            using (var stream = PostRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse ReturnResponce = (HttpWebResponse)PostRequest.GetResponse();
            return ReturnResponce.StatusCode.Equals(HttpStatusCode.OK);
        }
    }
}
