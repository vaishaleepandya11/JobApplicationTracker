using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.Interface
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(string fileName, Stream content, string contentType);
    }
}
