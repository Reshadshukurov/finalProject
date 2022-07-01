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
using System.IO;
using FinalProject.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AboutSectionController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly AppDbContext _context;

        public AboutSectionController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        // GET: Admin/AboutSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutSection.ToListAsync());
        }

        // GET: Admin/AboutSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutSection = await _context.AboutSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutSection == null)
            {
                return NotFound();
            }

            return View(aboutSection);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutSection = await _context.AboutSection.FindAsync(id);
            if (aboutSection == null)
            {
                return NotFound();
            }
            return View(aboutSection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,AboutSection aboutSection)
        {
            if (id != aboutSection.Id)
            {
                return NotFound();
            }

            if (aboutSection.Img != null)
            {
                if (!aboutSection.Img.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an image");
                    return View(aboutSection);
                }

                if(!aboutSection.Img.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too large");
                    return View(aboutSection);
                }

                  aboutSection.Image =await aboutSection.Img.Upload(env.WebRootPath + @"\img");
            }


            _context.Update(aboutSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutSectionExists(int id)
        {
            return _context.AboutSection.Any(e => e.Id == id);
        }
    }
}
