using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountController (SignInManager<ApplicationUser> singInMaganger,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = singInMaganger;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Models.ViewModels.RegisterUser formData)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    Email = formData.Email,
                    UserName = formData.Email,
                    DateRegistered = new DateTime(),
                    PhoneNumber = formData.Phone
                };

                var result = await _userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    List<Claim> claimList = new List<Claim>() {
                        new Claim(ClaimTypes.StateOrProvince,  $"{newUser.Location}", ClaimValueTypes.String),
                        new Claim(ClaimTypes.MobilePhone, newUser.PhoneNumber),
                        new Claim(ClaimTypes.Email, newUser.Email),
                        //new Claim(ClaimTypes.DateOfBirth, n00b.DateRegistered)
                    };
                    //Got tired of failing to debug this. Will come back later.
                    await _userManager.AddClaimsAsync(newUser, claimList);

                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    RedirectToAction("Index", "Home");
                }
            }
            return View(); // captcha stuff would go here in actual implementation
        }

        [HttpGet]
        public ViewResult Login()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (Models.ViewModels.WhoAreYou way)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(way.User, way.Password, false, false);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
