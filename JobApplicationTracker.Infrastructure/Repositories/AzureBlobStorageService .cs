using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using JobApplicationTracker.Application.Interface;
using Microsoft.Extensions.Configuration;

namespace JobApplicationTracker.Infrastructure.Repositories
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _container;
        public AzureBlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureWebJobsStorage"]!;
            _container = new BlobContainerClient(connectionString, "resumes");
        }
        public async Task<string> UploadAsync(string fileName, Stream content, string contentType)
        {
            await _container.CreateIfNotExistsAsync();

            var blobName = $"{Guid.NewGuid()}-{fileName}";
            var blob = _container.GetBlobClient(blobName);

            await blob.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobName;

        }
    }
}
