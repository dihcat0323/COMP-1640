using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace COMP___1640.Models
{
    public class IdeaUI
    {
        public int ideaId { get; set; }
        public string userName { get; set; }
        public string ideaTitle { get; set; }
        public string ideaContent { get; set; }
        public double postedDate { get; set; }
    }
}