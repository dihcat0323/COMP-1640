using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Entity
{
    public class TopicEntity
    {
        public int TotalPage { get; set; }
        public List<TopicUI> ListTopics { get; set; }
    }
}