using JobApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.DTOs
{
    public class ApplicationResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
