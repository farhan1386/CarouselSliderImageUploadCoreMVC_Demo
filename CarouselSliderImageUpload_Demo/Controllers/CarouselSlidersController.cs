using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarouselSliderImageUpload_Demo.Data;
using CarouselSliderImageUpload_Demo.Models;

namespace CarouselSliderImageUpload_Demo.Controllers
{
    public class CarouselSlidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarouselSlidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarouselSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarouselSliders.ToListAsync());
        }

        // GET: CarouselSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await _context.CarouselSliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselSlider == null)
            {
                return NotFound();
            }

            return View(carouselSlider);
        }

        // GET: CarouselSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarouselSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageName,ImagePath,Description")] CarouselSlider carouselSlider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carouselSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carouselSlider);
        }

        // GET: CarouselSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await _context.CarouselSliders.FindAsync(id);
            if (carouselSlider == null)
            {
                return NotFound();
            }
            return View(carouselSlider);
        }

        // POST: CarouselSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageName,ImagePath,Description")] CarouselSlider carouselSlider)
        {
            if (id != carouselSlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carouselSlider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarouselSliderExists(carouselSlider.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carouselSlider);
        }

        // GET: CarouselSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselSlider = await _context.CarouselSliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselSlider == null)
            {
                return NotFound();
            }

            return View(carouselSlider);
        }

        // POST: CarouselSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carouselSlider = await _context.CarouselSliders.FindAsync(id);
            _context.CarouselSliders.Remove(carouselSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarouselSliderExists(int id)
        {
            return _context.CarouselSliders.Any(e => e.Id == id);
        }
    }
}
