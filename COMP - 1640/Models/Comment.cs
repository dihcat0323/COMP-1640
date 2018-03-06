using System;

namespace COMP___1640.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public int ideaId { get; set; }
        public int personId { get; set; }
        public String commentDetails { get; set; }
        public Boolean isAnonymous { get; set; }
        public String commentDate { get; set; } //String for datetime?
    }
}