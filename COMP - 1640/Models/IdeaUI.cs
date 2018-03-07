using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace COMP___1640.Models
{
    [DataContract]
    public class IdeaUI
    {
        [DataMember(Name = "ideaId")]
        public int ideaId { get; set; }
        [DataMember(Name = "userName")]
        public string userName { get; set; }
        [DataMember(Name = "ideaTitle")]
        public string ideaTitle { get; set; }
        [DataMember(Name = "ideaContent")]
        public string ideaContent { get; set; }
        [DataMember(Name = "postedDate")]
        public double postedDate { get; set; }
    }
}