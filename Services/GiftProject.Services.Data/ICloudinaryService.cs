using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;

namespace GiftProject.Services.Data
{
    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file, string fileName);

        Task DeleteImage(Cloudinary cloudinary, string name);
    }
}
