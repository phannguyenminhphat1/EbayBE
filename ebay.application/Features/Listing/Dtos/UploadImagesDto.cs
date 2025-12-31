using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class UploadImagesDto
{
    [FromForm(Name = "images")]
    [Required(ErrorMessage = ListingMessages.IMAGES_IS_REQUIRED)]
    public List<IFormFile>? Images { get; set; }
}
