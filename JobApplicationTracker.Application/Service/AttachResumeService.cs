using JobApplicationTracker.Application.Interface;
using JobApplicationTracker.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Application.Service
{
    public class AttachResumeService
    {
        private readonly IApplicationRepository _repo;
        private readonly IBlobStorageService _blobStorage;
        public AttachResumeService(IApplicationRepository repo, IBlobStorageService blobStorage)
        {
            _repo = repo;
            _blobStorage = blobStorage;
        }
        public async Task<string?> ExecuteAsync(int applicationId, string fileName, Stream stream, string contentType)
        {
            var application = await _repo.GetByIdAsync(applicationId);
            if (application is null)
                return null;

            var blobName = await _blobStorage.UploadAsync(fileName, stream, contentType);

            application.ResumeBlobName = blobName;
            await _repo.UpdateAsync(application);

            return blobName;
        }
    }
}
