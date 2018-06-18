using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Week6Lab_Identity.Controllers
{
    public class InventoryController : Controller
    {
        private DbContext _context { get; set; }
        InventoryController (IServiceProvider ctx)
        {
            //_context = ctx; //TODO fix
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(_context.Words.All());
        //}
    }
}