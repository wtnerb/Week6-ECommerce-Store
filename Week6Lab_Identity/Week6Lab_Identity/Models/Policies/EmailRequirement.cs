using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.Policies
{
    public class EmailRequirement : IAuthorizationRequirement
    {
        public string Email { get; set; }

        /// <summary>
        /// Pass in a string to construct a regex of approved emails
        /// </summary>
        /// <param name="email"></param>
        public EmailRequirement(string email)
        {
            Email = email;
        }
    }
}
