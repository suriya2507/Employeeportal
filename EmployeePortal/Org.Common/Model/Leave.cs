using System;
using Org.Common.Domain;

namespace Org.Common.Model
{
    public class Leave
    {
        public int Id { get; set; }
        
        public string From { get; set; }
        public string To { get; set;}
        public LeaveStatus Status { get; set; }
        public string LeaveUserId { get; set; }

        public string message { get; set; }
    }
}