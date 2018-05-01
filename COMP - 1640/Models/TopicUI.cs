using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Models
{
    public class TopicUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string PostedDate { get; set; }
        public string ClosureDate { get; set; }
        public string FinalClosureDate { get; set; }
        public int TotalIdeas { get; set; }
        public string Percentages { get; set; }
    }
}