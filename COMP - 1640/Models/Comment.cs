using System;

namespace COMP___1640.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ideaId { get; set; }
        public int personId { get; set; }
        public string Details { get; set; }
        public bool isAnonymous { get; set; }
        public DateTime postedDate { get; set; }
    }
}