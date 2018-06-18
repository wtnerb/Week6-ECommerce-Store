using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): base(options) {}

        public DbSet<Word> Words { get; set; }
        public DbSet<BasketItem> Basket { get; set; }
        //TODO create BasketItem model before implementing
    }
}
