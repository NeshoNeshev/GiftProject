using System.Collections.Generic;

namespace GiftProject.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryExtensionService : ICloudinaryExtensionService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryExtensionService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }
        public async Task<string> UploadAsync(IFormFile file, string fileName)
        {
            string url = String.Empty;
            byte[] destinationImage;
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            await using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };
                var result = await cloudinary.UploadAsync(uploadParams);

                url = result.Uri.AbsoluteUri;
            }

            return url;
        }

        public async Task DeleteImage(Cloudinary cloudinary, string name)
        {
            var delParams = new DelResParams()
            {
                PublicIds = new List<string>() { name },
                Invalidate = true,
            };

            await cloudinary.DeleteResourcesAsync(delParams);
        }
    }
}
