using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.VewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        public HomeController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel hvm = new HomeViewModel
            {
                banner =await db.Banner.OrderByDescending(x=>x.Id).LastAsync(),
                aboutSection=await db.AboutSection.OrderByDescending(x=>x.Id).LastAsync(),
                services=await db.Services.OrderByDescending(x=>x.Id).Take(3).ToListAsync(),
                bigScreen=await db.BigScreen.OrderByDescending(x=>x.Id).LastAsync(),
                startSection=await db.StartSection.OrderByDescending(x => x.Id).LastAsync(),
                latestWork=await db.LatestWork.OrderByDescending(x => x.Id).LastAsync(),
                latestWorkImages=await db.LatestWorkImage.OrderByDescending(x=>x.Id).Take(6).ToListAsync(),
                pricing=await db.Pricing.OrderByDescending(x => x.Id).LastAsync(),
                blogs=await db.Blog.Take(3).ToListAsync(),
                HeaderFooterSide =await db.headerFooterSide.FindAsync(1)
            };
            return View(hvm);
        }

   
    }
}
