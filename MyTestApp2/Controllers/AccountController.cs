using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApp2.Controllers
{
    public class AccountController : Controller
    {
        private Microsoft.AspNetCore.Identity.UserManager<AppUser> UserMgr { get; }

        private Microsoft.AspNetCore.Identity.SignInManager<AppUser> SignInMgr { get; }

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
            Microsoft.AspNetCore.Identity.SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

            public async Task<IActionResult> Login()
        {
            var result = await SignInMgr.PasswordSignInAsync("TestUser", "Test123!", false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "result is: " + result.ToString();
            }

            return View();
        }



            public async Task<IActionResult> Register()
        {
            try
            {
                ViewBag.Message = "User already registred";

                AppUser user = await UserMgr.FindByNameAsync("TestUser");

                if(user == null)
                {
                    user = new AppUser();
                    user.UserName = "TestUser";
                    user.Email = "TestUser@test.com";
                    user.FirstName = "Kaio";
                    user.LastName = "Garcia";

                    IdentityResult result = await UserMgr.CreateAsync(user, "Test123!");

                    ViewBag.Message = "User was created";

                }

            } catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

    }
}
