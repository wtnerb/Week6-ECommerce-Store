using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class Basket
    {
        public List<ItemInBasket> Items { get; set; }
        public decimal Total { get
            {
                decimal total = 0;
                foreach(ItemInBasket item in Items)
                {
                    total +=  item.Item.Price * item.Quantity;
                }
                return total;
            }
        }

        public Basket(List<ItemInBasket> basket)
        {
            Items = basket;
        }
    }

    public struct ItemInBasket
    {
        public Word Item { get; set; }
        public int Quantity { get; set; }
    }
}
