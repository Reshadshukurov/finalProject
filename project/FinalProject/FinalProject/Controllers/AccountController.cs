using FinalProject.Models;
using FinalProject.VewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser
            {
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                UserName = viewModel.Surname,
                Email = viewModel.Email,
            };

            IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(viewModel);
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser loginUser = await userManager.FindByEmailAsync(viewModel.Email);
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Email  or Password is wrong");
                return View(viewModel);
            }

           Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(loginUser, viewModel.Password, viewModel.RememberMe, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "you are lock out ,pls wait 30 seconds");
                }
                else
                {
                    ModelState.AddModelError("", "Email  or Password is wrong");
                }
                return View(viewModel);
            }
            if ((await userManager.GetRolesAsync(loginUser)).Count > 0 && ((await userManager.GetRolesAsync(loginUser))[0] == "Admin" || (await userManager.GetRolesAsync(loginUser))[0] == "SuperAdmin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }


        //public async Task<IActionResult> CreatrRoles()
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });

        //    return Content("Ok"); 
        //}

        //public async Task<IActionResult> AddRoleToUser()
        //{
        //    AppUser user = await userManager.FindByNameAsync("Azizov");
        //    await userManager.AddToRoleAsync(user,"SuperAdmin");

        //    AppUser user2 = await userManager.FindByNameAsync("Shukurov");
        //    await userManager.AddToRoleAsync(user2, "Admin");

        //    AppUser user3 = await userManager.FindByNameAsync("Azizov");
        //    await userManager.AddToRoleAsync(user3, "Member");

        //    return Content("Ok");
        //}
    }
}
