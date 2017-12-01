using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utills
{
    public class MiscUtills
    {
        /// <summary>
        /// Converts from d/M/yyyy hh:mm:ss tt (Html <input type="datetime"></input> format) to valid <see cref="System.DateTime"/>
        /// <todo> maybe better to implement an <see cref="IFormattable"/> ? </todo>
        /// </summary>
        /// <param name="StringDateTime">The String to Convert</param>
        /// 
        /// <returns> a <see cref="System.DateTime"/> Object </returns>
        public static System.DateTime ConvertFromHtmlDateTimeFormat(System.String StringDateTime)
        {
            return DateTime.ParseExact(StringDateTime, "d/M/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
