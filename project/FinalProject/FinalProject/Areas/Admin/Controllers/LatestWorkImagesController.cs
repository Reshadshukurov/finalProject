using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using FinalProject.Extensions;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class LatestWorkImagesController : Controller
    {

        private readonly IWebHostEnvironment env;
        private readonly AppDbContext _context;

        public LatestWorkImagesController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.LatestWorkImage.ToListAsync());
        }

        // GET: Admin/LatestWorkImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latestWorkImage = await _context.LatestWorkImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latestWorkImage == null)
            {
                return NotFound();
            }

            return View(latestWorkImage);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LatestWorkImage latestWorkImage)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (latestWorkImage.Img == null)
            {
                ModelState.AddModelError("Img", "Input is empty");
                return View();
            }

            if (!latestWorkImage.Img.isImage())
            {
                ModelState.AddModelError("Img", "File is not an Image");
                return View();
            }
            if (!latestWorkImage.Img.isSmallerThan(1048576))
            {
                ModelState.AddModelError("Img", "File is too big");
                return View();
            }

            latestWorkImage.Image =await latestWorkImage.Img.Upload(env.WebRootPath + @"\img");

                _context.Add(latestWorkImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/LatestWorkImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latestWorkImage = await _context.LatestWorkImage.FindAsync(id);
            if (latestWorkImage == null)
            {
                return NotFound();
            }
            return View(latestWorkImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,LatestWorkImage latestWorkImage)
        {
            if (id != latestWorkImage.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (latestWorkImage.Img != null)
            {
                if (!latestWorkImage.Img.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an Image");
                    return View();
                }
                if (!latestWorkImage.Img.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too big");
                    return View();
                }

                if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", latestWorkImage.Image)))
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", latestWorkImage.Image));
                }

                latestWorkImage.Image = await latestWorkImage.Img.Upload(env.WebRootPath + @"\img");
            }

                    _context.Update(latestWorkImage);
                    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

               
        }

        // GET: Admin/LatestWorkImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latestWorkImage = await _context.LatestWorkImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latestWorkImage == null)
            {
                return NotFound();
            }

            return View(latestWorkImage);
        }

        // POST: Admin/LatestWorkImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var latestWorkImage = await _context.LatestWorkImage.FindAsync(id);

            if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", latestWorkImage.Image)))
            {
                System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", latestWorkImage.Image));
            }
            _context.LatestWorkImage.Remove(latestWorkImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LatestWorkImageExists(int id)
        {
            return _context.LatestWorkImage.Any(e => e.Id == id);
        }
    }
}
