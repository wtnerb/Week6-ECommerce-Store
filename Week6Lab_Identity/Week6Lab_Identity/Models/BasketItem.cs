using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string UserKey { get; set; }
        public int UserBasketNum { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
    }
}
