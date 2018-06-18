using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week6Lab_Identity.Data;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreContext _context;

        public AdminController(SignInManager<ApplicationUser> signInMaganger,
            UserManager<ApplicationUser> userManager,
            StoreContext context)
        {
            _signInManager = signInMaganger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditUsers()
        {
            return View(_userManager.Users);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            //TODO test this
            ApplicationUser u = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(u);
            return RedirectToAction("EditUsers");
        }
        
        [HttpGet]
        public async Task<IActionResult> EditUser (int id)
        {
            ApplicationUser u = await _userManager.FindByIdAsync(id.ToString());
            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser (ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return RedirectToAction("EditUsers");
        }

        [HttpGet]
        public IActionResult CreateWord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord (Word word)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error"] = "That word could not be created";
                return View();
            }

            await _context.Words.AddAsync(word);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditInventory), nameof(AdminController));
        }

        [HttpGet]
        public async Task<IActionResult> EditInventory()
        {
            return View(await _context.Words.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> EditWord(Word word)
        {
            //TODO make work
            return Ok();
        }
    }
}