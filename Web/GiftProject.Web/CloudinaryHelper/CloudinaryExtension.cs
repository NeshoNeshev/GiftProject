namespace GiftProject.Web.CloudinaryHelper
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryExtension
    {
        public static async Task<string> UploadAsync(Cloudinary cloudinary, IFormFile file)
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
    }
}
