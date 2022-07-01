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
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HeaderFooterSideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public HeaderFooterSideController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        // GET: Admin/HeaderFooterSide
        public async Task<IActionResult> Index()
        {
            return View(await _context.headerFooterSide.ToListAsync());
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headerFooterSide = await _context.headerFooterSide.FindAsync(id);
            if (headerFooterSide == null)
            {
                return NotFound();
            }
            return View(headerFooterSide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,HeaderFooterSide headerFooterSide)
        {
            if (id != headerFooterSide.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (headerFooterSide.HeaderLogoImg != null)
            {
                if (!headerFooterSide.HeaderLogoImg.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an image");
                    return View();
                }
                if (!headerFooterSide.HeaderLogoImg.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too big");
                    return View();
                }
                if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", headerFooterSide.HeaderLogo)))
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", headerFooterSide.HeaderLogo));
                }

                headerFooterSide.HeaderLogo = await headerFooterSide.HeaderLogoImg.Upload(env.WebRootPath + @"\img");
            }

            if (headerFooterSide.FooterLogoImg != null)
            {
                if (!headerFooterSide.FooterLogoImg.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an image");
                    return View();
                }

                if (!headerFooterSide.FooterLogoImg.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too big");
                    return View();
                }

                if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", headerFooterSide.FooterLogo)))
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", headerFooterSide.FooterLogo));
                }

                headerFooterSide.FooterLogo = await headerFooterSide.FooterLogoImg.Upload(env.WebRootPath + @"\img");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headerFooterSide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderFooterSideExists(headerFooterSide.Id))
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
            return View(headerFooterSide);
        }

        private bool HeaderFooterSideExists(int id)
        {
            return _context.headerFooterSide.Any(e => e.Id == id);
        }
    }
}
