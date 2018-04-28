using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal EstimatedLifetimeEarnings { get; set; }
        public DateTime DateRegistered { get; set; }
        public byte Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}
