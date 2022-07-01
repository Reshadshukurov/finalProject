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
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public BlogsController(AppDbContext context, IWebHostEnvironment _env)
        {
            _context = context;
            env = _env;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.Count = Math.Ceiling((double)_context.Blog.Count()) / (double)2;
            ViewBag.CurrentPage = page;
            int skip = (page - 1) * 2;
            return View(await _context.Blog.Skip(skip).Take(2).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!blog.NewsImg.isImage())
            {
                ModelState.AddModelError("Img", "File is not an image");
                return View();
            }
            if (!blog.PostImg.isImage())
            {
                ModelState.AddModelError("Img", "File is not an image");
                return View();
            }

            if (!blog.NewsImg.isSmallerThan(1048576))
            {
                ModelState.AddModelError("Img", "File is too big");
                return View();
            }

            if (!blog.PostImg.isSmallerThan(1048576))
            {
                ModelState.AddModelError("Img", "File is too big");
                return View();
            }

            blog.NewsImage = await blog.NewsImg.Upload(env.WebRootPath + @"\img");
            blog.PostImage = await blog.PostImg.Upload(env.WebRootPath + @"\img");

            _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (blog.NewsImg != null)
            {
                if (!blog.NewsImg.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an image");
                    return View();
                }
                if (!blog.NewsImg.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too big");
                    return View();
                }
                if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", blog.NewsImage)))
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", blog.NewsImage));
                }

                blog.NewsImage = await blog.NewsImg.Upload(env.WebRootPath + @"\img");
            }

            if (blog.PostImg != null)
            {
                if (!blog.PostImg.isImage())
                {
                    ModelState.AddModelError("Img", "File is not an image");
                    return View();
                }

                if (!blog.PostImg.isSmallerThan(1048576))
                {
                    ModelState.AddModelError("Img", "File is too big");
                    return View();
                }

                if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", blog.PostImage)))
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", blog.PostImage));
                }

                blog.PostImage = await blog.PostImg.Upload(env.WebRootPath + @"\img");

            }

            _context.Update(blog);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.FindAsync(id);

            if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", blog.PostImage)))
            {
                System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", blog.PostImage));
            }

            if (System.IO.File.Exists(Path.Combine(env.WebRootPath + @"\img", blog.NewsImage)))
            {
                System.IO.File.Delete(Path.Combine(env.WebRootPath + @"\img", blog.NewsImage));
            }

            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
