using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ClosureDate { get; set; }
        public DateTime? FinalClosureDate { get; set; }
    }
}