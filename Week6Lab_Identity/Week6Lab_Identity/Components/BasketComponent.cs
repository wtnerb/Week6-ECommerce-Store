using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;
using Week6Lab_Identity.Models;
using Week6Lab_Identity.Models.ViewModels;

namespace Week6Lab_Identity.Components
{

    [ViewComponent]
    public class BasketComponent : ViewComponent
    {
        private StoreContext _context { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BasketComponent (StoreContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync ()
        {
            ApplicationUser current = await _signInManager.UserManager.GetUserAsync(new System.Security.Claims.ClaimsPrincipal(User));

            List<ItemInBasket> items = new List<ItemInBasket>();
            var userBasket = _context.Basket.Where(x => x.UserBasketNum == current.BasketId && x.UserKey == current.Id);
            foreach (var item in userBasket)
            {
                ItemInBasket basketItem = new ItemInBasket
                {
                    Item = _context.Words.FirstOrDefault(x => x.Id == item.ItemId),
                    Quantity = item.ItemQuantity
                };
                items.Add(basketItem);
            }
            Basket basket = new Basket(items);
            return View(basket);
        }
    }
}
