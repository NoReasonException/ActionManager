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
        public static System.String SubmitNewCustomer_FormEncoder(System.Collections.Generic.Dictionary<System.String,System.String> FormData)
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
            }catch (KeyNotFoundException e)
            {
                Debug.WriteLine("SubmitNewCustomer_FormEncoder encountered an error -> KeyNot Found , ITS A BUUUUGGG!!! you check the existance in NewUserController.cs:PostNew()");
                Debug.WriteLine("Read Carefully! :" + e.Message);
                throw new InvalidOperationException("Caused By :", e);
            }
        }
    }
}
