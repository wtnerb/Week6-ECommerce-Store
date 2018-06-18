using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreContext _context;
        
        public AccountController (SignInManager<ApplicationUser> signInMaganger,
            UserManager<ApplicationUser> userManager,
            StoreContext context)
        {
            _signInManager = signInMaganger;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register ()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.ViewModels.RegisterUser formData)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser newUser = new ApplicationUser
                {
                    Email = formData.Email,
                    UserName = formData.Email,
                    DateRegistered = DateTime.Now,
                    PhoneNumber = formData.Phone,
                    Location = (byte)Enum.Parse<Region>(formData.Location),
                    BasketId = 0
                };

                var result = await _userManager.CreateAsync(newUser, formData.ConfirmPassword);

                if (result.Succeeded)
                {
                    List<Claim> claimList = new List<Claim>() {
                        new Claim(ClaimTypes.StateOrProvince,  $"{newUser.Location}", ClaimValueTypes.String),
                        new Claim(ClaimTypes.MobilePhone, newUser.PhoneNumber),
                        new Claim(ClaimTypes.Email, newUser.Email, ClaimValueTypes.Email),
                        new Claim(ClaimTypes.DateOfBirth, new DateTime(newUser.DateRegistered.Year, newUser.DateRegistered.Month, newUser.DateRegistered.Day).ToString("u"), ClaimValueTypes.DateTime)
                    };
                    //Got tired of failing to debug this. Will come back later.
                    await _userManager.AddClaimsAsync(newUser, claimList);

                    await _userManager.AddToRoleAsync(newUser, Purpose.User);

                    await _signInManager.SignInAsync(newUser, isPersistent: false);


                    RedirectToAction("Prods", "Store");
                }
            }
            return View(); // captcha stuff would go here in actual implementation
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            //Following line taken from Amanda's example code
            //Closes any optn accounts and prevent complications with login (if a user manually
            //navigates to login page while already logged in, for example)
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (Models.ViewModels.Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Store");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        //TODO create make admin, demote admin routes for view to hit
        //TODO create details route that displays details of current user
    }
}
