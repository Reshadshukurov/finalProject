using FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}

        public DbSet<Banner> Banner { get; set; }
        public DbSet<AboutSection> AboutSection { get; set; }
        public DbSet<BigScreen> BigScreen { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<StartSection> StartSection { get; set; }
        public DbSet<LatestWork> LatestWork { get; set; }
        public DbSet<LatestWorkImage> LatestWorkImage { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<HeaderFooterSide> headerFooterSide { get; set; }
    }
}
