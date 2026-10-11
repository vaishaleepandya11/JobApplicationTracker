using JobApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.DTOs
{
    public class UpdateStatusRequest
    {
        public ApplicationStatus Status { get; set; }
    }
}
