using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Entity
{
    public class IdeaEntity
    {
        public int TotalPage { get; set; }
        public List<IdeaUI> ListIdeas { get; set; }
    }
}