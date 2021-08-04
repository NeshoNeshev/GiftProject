namespace GiftProject.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryExtensionService
    {
        public Task<string> UploadAsync(IFormFile file, string fileName);
    }
}
