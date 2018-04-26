 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    EstimatedLifetimeEarnings =
                        (decimal)((double)noob.CurrentIncome * (noob.Age - 10) * 0.7)
                };

                var result = await _userManager.CreateAsync(n00b);

                if (result.Succeeded)
                {
                    List<Claim> areYouReal = new List<Claim>();
                    Claim wealth = new Claim(ClaimTypes.)
                }
            }
        }
    }
}
