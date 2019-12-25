using System.ComponentModel.DataAnnotations;

namespace CarouselSliderImageUpload_Demo.Models
{
    public class CarouselSlider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter image name")]
        [Display(Name = "Image Name")]
        [StringLength(100)]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please enter image description")]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
