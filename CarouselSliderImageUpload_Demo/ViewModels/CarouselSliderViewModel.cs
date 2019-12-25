using System.ComponentModel.DataAnnotations;

namespace CarouselSliderImageUpload_Demo.ViewModels
{
    public class CarouselSliderViewModel : EditImage
    {
        [Required(ErrorMessage = "Please enter image description")]
        public string Description { get; set; }
    }
}
