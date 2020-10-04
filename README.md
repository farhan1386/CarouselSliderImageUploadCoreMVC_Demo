# ASP.NET CORE MVC CURD OPERATION WITH IMAGE UPLOAD

This demo application with explain you how you can upload image, edit image and delete image using ASP.NET Core MVC.

**Step-1** From the Visual Studio select Create a new project. Select ASP.NET Core Web Application and then select Next. Name the project and select Create.Select Web Application(Model-View-Controller), and then select Create.Visual Studio used the default template for the MVC project you just created. You have a working app right now by entering a project name and selecting a few options. This is a basic starter project.

**Step-2** Create Model CarouselSlider class
```
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
```
**Step-3** In Solution Explorer, right-click Controllers > Add > Controller. In the Add Scaffold dialog box, select Controller Class - Model Class and Data context class  and select ADD.

**Step-4** In Solution Explorer, Create ViewModel folder. Create ViewModel Classes CarouselSliderViewModel, UploadImage and EditImage.

**Step-5** In Solution Explorer under wwwroot create folder images to upload images. 



