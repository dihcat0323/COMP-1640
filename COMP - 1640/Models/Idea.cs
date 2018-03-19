using System;

namespace COMP___1640.Models
{
    public class Idea
    {
        public int Id { get; set; }
        public int topicId { get; set; }
        public int CategoryId { get; set; }
        public int PersonalId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string DocumentLink { get; set; }
        public int isAnonymous { get; set; }
        public int TotalViews { get; set; }
        public DateTime? PostedDate { get; set; }
        //public DateTime? ClosureDate { get; set; }
    }
}