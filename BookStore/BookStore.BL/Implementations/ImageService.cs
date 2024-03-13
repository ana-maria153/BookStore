using BookStore.BL.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Implementations
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }

        public ImageUploadResult AddPhoto (IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                };
                uploadResult = _cloudinary.Upload (uploadParams);
            }
            return uploadResult;
        }

        public DeletionResult DeletePhoto (string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var deletionResult = _cloudinary.Destroy(deleteParams);
            return deletionResult;
        }
    }
}
