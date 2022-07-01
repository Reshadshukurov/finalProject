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
    public class BigScreenController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public BigScreenController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        // GET: Admin/BigScreen
        public async Task<IActionResult> Index()
        {
            return View(await _context.BigScreen.ToListAsync());
        }

        // GET: Admin/BigScreen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bigScreen = await _context.BigScreen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bigScreen == null)
            {
                return NotFound();
            }

            return View(bigScreen);
        }

        // GET: Admin/BigScreen/Create
        

        // GET: Admin/BigScreen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bigScreen = await _context.BigScreen.FindAsync(id);
            if (bigScreen == null)
            {
                return NotFound();
            }
            return View(bigScreen);
        }

        // POST: Admin/BigScreen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BigScreen bigScreen)
        {
            if (id != bigScreen.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!bigScreen.Img.isImage())
            {
                ModelState.AddModelError("Img", "File is not an image");
                return View(bigScreen);
            }
            if(!bigScreen.Img.isSmallerThan(1048576))
            {
                ModelState.AddModelError("Img", "File is too big");
                return View(bigScreen);
            }

            if (bigScreen.Img != null)
            {
                bigScreen.Image = await bigScreen.Img.Upload(env.WebRootPath + @"\img");
            }

            _context.Update(bigScreen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BigScreenExists(int id)
        {
            return _context.BigScreen.Any(e => e.Id == id);
        }
    }
}
