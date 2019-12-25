using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarouselSliderImageUpload_Demo.ViewModels
{
    public class UploadImage
    {
        [Required(ErrorMessage = "Please enter image name")]
        [Display(Name = "Image Name")]
        [StringLength(100)]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Upload Image")]
        public IFormFile Image { get; set; }
    }
}
