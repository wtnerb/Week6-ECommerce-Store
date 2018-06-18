using Microsoft.AspNetCore.Identity;
using System;

namespace Week6Lab_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateRegistered { get; set; }
        public byte Location { get; set; }
        public int BasketId { get; set; }
    }

    public static class Purpose
    {
        //By keeping these strings here, it is easier to not accidentally mispell them later.
        public const string Admin = "admin";
        public const string User = "user";
    }
}
