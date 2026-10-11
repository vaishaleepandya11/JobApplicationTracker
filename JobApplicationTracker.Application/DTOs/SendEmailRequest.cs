using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.DTOs
{
    public class SendEmailRequest
    {
        public string to { get; set; }

        public string subject { get; set; }

        public string body { get; set; }
    }
}
