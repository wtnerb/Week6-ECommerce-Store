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
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.ViewModels.CreateUser noob)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser n00b = new ApplicationUser
                {
                    Email = noob.Email,
                    UserName = noob.Email,
                    DateRegistered = new DateTime()
                };

                var result = await _userManager.CreateAsync(n00b);

                if (result.Succeeded)
                {
                    Regex edu = new Regex(".edu$");//one claims are working, use to make a claim to student status
                    List<Claim> areYouReal = new List<Claim>();
                    //Got tired of failing to debug this. Will come back later.
                    Claim wealth = new Claim(ClaimTypes.StateOrProvince,  $"{n00b.Location}", ClaimValueTypes.String);
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
            //Finally looked at the example code and realized I could do all this in a 
            //one line method call. oops.
            //if (ModelState.IsValid)
            //{
            //    var user = _userManager.Users.FirstOrDefault(x => x.Email == way.User);
            //    if (user == null)
            //    {
            //        return RedirectToAction("Home", "Index");//Do something more useful here
            //    }
            //    if ( await _userManager.CheckPasswordAsync(user, way.Password))
            //    {
            //        _userManager.
            //    }
            //}
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(way.User, way.Password, false, false);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
