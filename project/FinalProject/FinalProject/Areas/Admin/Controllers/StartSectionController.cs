using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class StartSectionController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly AppDbContext _context;

        public StartSectionController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        // GET: Admin/StartSection
        public async Task<IActionResult> Index()
        {
            return View(await _context.StartSection.ToListAsync());
        }

        // GET: Admin/StartSection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startSection = await _context.StartSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (startSection == null)
            {
                return NotFound();
            }

            return View(startSection);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startSection = await _context.StartSection.FindAsync(id);
            if (startSection == null)
            {
                return NotFound();
            }
            return View(startSection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,StartSection startSection)
        {
            if (id != startSection.Id)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View();
            }

            if (startSection.Img != null)
            {
                if (!startSection.Img.isImage())
                {
                    ModelState.AddModelError("img", "File is not an image");
                    return View(startSection);
                }

                if (!startSection.Img.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too large");
                    return View(startSection);
                }

                startSection.Image = await startSection.Img.Upload(env.WebRootPath + @"\img");
            }

            _context.Update(startSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

      
        private bool StartSectionExists(int id)
        {
            return _context.StartSection.Any(e => e.Id == id);
        }
    }
}
