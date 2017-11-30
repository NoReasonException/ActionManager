using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProject.Utills
{
    public class Utills
    {
        public static System.String IFNULL(System.Object toStr)
        {
            if (toStr == null) return "NULL";
            return toStr.ToString();
        }
        public static bool isDateTimesOkay(System.DateTime StartDate, System.DateTime EndDate)
        {
            return StartDate != null && EndDate != null && System.DateTime.Compare(StartDate, EndDate) < 0;
        }
    }
}