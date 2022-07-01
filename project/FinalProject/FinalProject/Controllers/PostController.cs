using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext db;
        public PostController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index(int? Id)
        {
            if (Id != null)
            {
                Blog blog =  db.Blog.FirstOrDefault(x => x.Id == Id);
                return View(blog);
            }
            return View();
        }
    }
}
