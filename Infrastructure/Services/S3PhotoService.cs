using Amazon.S3;
using Amazon.S3.Transfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class S3PhotoService : IPhotoService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "adhikademy";

        public S3PhotoService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadProfilePhotoAsync(Stream fileStream, string fileName)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = _bucketName,
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
