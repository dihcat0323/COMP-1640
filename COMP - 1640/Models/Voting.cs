using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Models
{
    public class Voting
    {
        public int IdeaId { get; set; }
        public int PersonId { get; set; }
        public string Vote { get; set; }
    }
}