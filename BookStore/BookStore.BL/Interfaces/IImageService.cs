using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BookStore.BL.Interfaces
{
    public interface IImageService
    {
        ImageUploadResult AddPhoto(IFormFile file);
        DeletionResult DeletePhoto(string publicId);
    }
}
