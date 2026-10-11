using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.DTOs
{
    public class AddApplicationRequest
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
