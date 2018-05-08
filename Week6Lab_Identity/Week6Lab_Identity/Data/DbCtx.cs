using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity.Data
{
    public class DbCtx : IdentityDbContext<ApplicationUser>
    {

        public DbCtx(DbContextOptions<DbCtx> options) : base(options)
        {
        }
    }
}
