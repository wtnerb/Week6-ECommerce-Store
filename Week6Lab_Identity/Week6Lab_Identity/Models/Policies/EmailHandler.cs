using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.Policies
{
    public class EmailHandler : AuthorizationHandler<EmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailRequirement requirement)
        {
            Regex rx = new Regex(requirement.Email);
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            string email = context.User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            if (rx.IsMatch(email))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
