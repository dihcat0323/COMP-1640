using System;

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