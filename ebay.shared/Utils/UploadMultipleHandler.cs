using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class UploadMultipleHandler
{
    private static Cloudinary GetCloudinaryInstance()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var cloudName = configuration["AppSettings:CloudinaryName"];
        var apiKey = configuration["AppSettings:CloudinaryApiKey"];
        var apiSecret = configuration["AppSettings:CloudinaryApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        return new Cloudinary(account);
    }
    public static bool UploadMultipleFiles<T>(List<IFormFile> files, out List<string> urls, out ResponseService<T>? errorResponse)
    {
        errorResponse = null;
        urls = new();

        if (files == null || files.Count == 0)
        {
            errorResponse = new ResponseService<T>(
                (int)HttpStatusCode.BadRequest,
                "Images are required"
            );
            return false;
        }
        List<string> validExtension = new List<string>()
        {
            ".jpg",
            ".png",
            ".jpeg"
        };
        var cloudinary = GetCloudinaryInstance();

        foreach (var file in files)
        {

            if (file == null)
            {
                errorResponse = new ResponseService<T>(
                    (int)HttpStatusCode.BadRequest,
                    "Invalid file"
                );
                return false;
            }
            // Check extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!validExtension.Contains(extension))
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: $"{CommonMessages.INVALID_EXTENSION} - {CommonMessages.REQUIRED_EXTENSION}{string.Join(",", validExtension)}"
                );
                return false;
            }
            // Check File size
            if (file.Length > 1 * 1024 * 1024)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: CommonMessages.MAXIMUM_FILE_SIZE
                );
                return false;
            }
            try
            {
                using var stream = file.OpenReadStream();

                // Change name
                string fileName = Guid.NewGuid().ToString();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName + extension, stream),
                    PublicId = fileName,
                    Overwrite = false
                };
                var result = cloudinary.Upload(uploadParams);
                if (result.SecureUrl == null)
                {
                    errorResponse = new ResponseService<T>(
                        (int)HttpStatusCode.InternalServerError,
                        "Upload fail"
                    );
                    return false;
                }
                urls.Add(result.SecureUrl.ToString());
            }
            catch (Exception ex)
            {
                errorResponse = new ResponseService<T>(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    message: "Upload thất bại: " + ex.Message
                );
                return false;
            }
        }
        return true;
    }
}