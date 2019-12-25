using CarouselSliderImageUpload_Demo.Data;
using CarouselSliderImageUpload_Demo.Models;
using CarouselSliderImageUpload_Demo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CarouselSliderImageUpload_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var carouselSlider = await dbContext.CarouselSliders.ToListAsync();
            return View(carouselSlider);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await dbContext.CarouselSliders.FindAsync(id);
            var carouselViewModel = new CarouselSliderViewModel()
            {
                Id = carouselSlider.Id,
                ImageName = carouselSlider.ImageName,
                Description = carouselSlider.Description,
                ExistingImage = carouselSlider.ImagePath
            };

            if (carouselSlider == null)
            {
                return NotFound();
            }
            return View(carouselViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselSliderViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                CarouselSlider carouselSlider = new CarouselSlider
                {
                    ImageName = model.ImageName,
                    ImagePath = uniqueFileName,
                    Description = model.Description
                };
                dbContext.Add(carouselSlider);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await dbContext.CarouselSliders.FindAsync(id);
            var carouselViewModel = new CarouselSliderViewModel()
            {
                Id = carouselSlider.Id,
                ImageName = carouselSlider.ImageName,
                Description = carouselSlider.Description,
                ExistingImage = carouselSlider.ImagePath
            };

            if (carouselSlider == null)
            {
                return NotFound();
            }
            return View(carouselViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarouselSliderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carouselSlider = await dbContext.CarouselSliders.FindAsync(model.Id);
                carouselSlider.Description = model.Description;
                carouselSlider.ImageName = model.ImageName;

                if (model.Image != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    carouselSlider.ImagePath = ProcessUploadedFile(model);
                }
                dbContext.Update(carouselSlider);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await dbContext.CarouselSliders.FindAsync(id);
            var carouselViewModel = new CarouselSliderViewModel()
            {
                Id = carouselSlider.Id,
                ImageName = carouselSlider.ImageName,
                Description = carouselSlider.Description,
                ExistingImage = carouselSlider.ImagePath
            };

            if (carouselSlider == null)
            {
                return NotFound();
            }
            return View(carouselViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carouselSlider = await dbContext.CarouselSliders.FindAsync(id);
            dbContext.CarouselSliders.Remove(carouselSlider);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadedFile(CarouselSliderViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
