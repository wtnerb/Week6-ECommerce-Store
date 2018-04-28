 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
        public async Task<IActionResult> Register(Models.ViewModels.RegisterUser noob)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser n00b = new ApplicationUser
                {
                    Email = noob.Email,
                    UserName = noob.Email,
                    DateRegistered = new DateTime(),
                    PhoneNumber = noob.Phone
                };

                var result = await _userManager.CreateAsync(n00b);

                if (result.Succeeded)
                {
                    Regex edu = new Regex(".edu$");//one claims are working, use to make a claim to student status
                    List<Claim> areYouReal = new List<Claim>() {
                        new Claim(ClaimTypes.StateOrProvince,  $"{n00b.Location}", ClaimValueTypes.String),
                        new Claim(ClaimTypes.MobilePhone, n00b.PhoneNumber),
                        new Claim(ClaimTypes.Email, n00b.Email),
                        //new Claim(ClaimTypes.DateOfBirth, n00b.DateRegistered)
                    };
                    //Got tired of failing to debug this. Will come back later.
                    await _userManager.AddClaimsAsync(n00b, areYouReal);

                    await _signInManager.SignInAsync(n00b, isPersistent: false);

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
