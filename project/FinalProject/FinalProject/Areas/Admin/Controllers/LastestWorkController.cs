using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class LastestWorkController : Controller
    {
        private readonly AppDbContext _context;

        public LastestWorkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LastestWork
        public async Task<IActionResult> Index()
        {
            return View(await _context.LatestWork.ToListAsync());
        }

        // GET: Admin/LastestWork/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latestWork = await _context.LatestWork
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latestWork == null)
            {
                return NotFound();
            }

            return View(latestWork);
        }

        // GET: Admin/LastestWork/Create
       

        // GET: Admin/LastestWork/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latestWork = await _context.LatestWork.FindAsync(id);
            if (latestWork == null)
            {
                return NotFound();
            }
            return View(latestWork);
        }

        // POST: Admin/LastestWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,LatestWork latestWork)
        {
            if (id != latestWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(latestWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LatestWorkExists(latestWork.Id))
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
            return View(latestWork);
        }

       
        private bool LatestWorkExists(int id)
        {
            return _context.LatestWork.Any(e => e.Id == id);
        }
    }
}
