using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class UploadHandler
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
    public static bool UploadFile<T>(IFormFile file, out string? urlString, out ResponseService<T>? errorResponse)
    {
        errorResponse = null;
        urlString = null;
        List<string> validExtension = new List<string>()
        {
            ".jpg",
            ".png",
            ".jpeg"
        };
        // Check extension
        string extension = Path.GetExtension(file.FileName);
        if (!validExtension.Contains(extension))
        {
            errorResponse = new ResponseService<T>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: $"{CommonMessages.INVALID_EXTENSION} - {CommonMessages.REQUIRED_EXTENSION}{string.Join(",", validExtension)}"
            );
            return false;
        }

        // Check File size
        long size = file.Length;
        if (size > 1 * 1024 * 1024)
        {
            errorResponse = new ResponseService<T>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: CommonMessages.MAXIMUM_FILE_SIZE
            );
            return false;
        }
        try
        {
            var cloudinary = GetCloudinaryInstance();
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
            urlString = result.SecureUrl?.ToString();
            return true;
        }
        catch (Exception ex)
        {
            errorResponse = new ResponseService<T>(
                statusCode: (int)HttpStatusCode.InternalServerError,
                 message: "Upload thất bại: " + ex.Message
            );
            return false;
            throw;
        }

        // string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        // if (!Directory.Exists(path))
        // {
        //     Directory.CreateDirectory(path);
        // }
        // using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
        // file.CopyTo(stream);
        // urlString = fileName;
        // return true;
    }
}