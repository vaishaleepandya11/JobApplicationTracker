using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? LastFollowUpDate { get; set; }

        public string? ResumeBlobName { get; set; }
    }
    public enum ApplicationStatus
    {
        Applied,
        InterviewScheduled,
        Rejected,
        OfferReceived
    }
}
