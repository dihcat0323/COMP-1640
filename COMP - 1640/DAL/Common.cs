using System;

namespace COMP___1640.DAL
{
    public class Common
    {
        public double CalculateDateRange(DateTime? dt)
        {
            var date = Convert.ToDateTime(dt);
            return (DateTime.Today - date).TotalDays;
        }
    }
}