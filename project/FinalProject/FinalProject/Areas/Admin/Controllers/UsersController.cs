using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UsersController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> Block(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            user.Status = false;

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnBlock(string Id)
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            user.Status = true;

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Manageroles(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Manageroles(string Id, bool setMember, bool setAdmin, bool setSuperAdmin)
        {
            AppUser user = await userManager.FindByIdAsync(Id);

            var roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles.ToArray());

            if (setMember) await userManager.AddToRoleAsync(user, "Member");
            if (setAdmin) await userManager.AddToRoleAsync(user, "Admin");
            if (setSuperAdmin) await userManager.AddToRoleAsync(user, "SuperAdmin");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ResetPassword(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, token, password);
            return RedirectToAction("Index");
        }
    }
}
