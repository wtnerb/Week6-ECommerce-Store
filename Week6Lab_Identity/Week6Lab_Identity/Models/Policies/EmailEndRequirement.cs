using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.Policies
{
    public class EmailEndRequirement : IAuthorizationRequirement
    {
        public string EmailEnd { get; set; }

        public EmailEndRequirement(string emailEnd)
        {
            EmailEnd = emailEnd;
        }
    }
}
