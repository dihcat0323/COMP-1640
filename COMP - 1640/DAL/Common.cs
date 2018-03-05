using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.DAL
{
    public class Common
    {
        public double CalculatePostedDate(DateTime? postedDate)
        {
            var date = Convert.ToDateTime(postedDate);
            return (DateTime.Today - date).TotalDays;
        }
    }
}