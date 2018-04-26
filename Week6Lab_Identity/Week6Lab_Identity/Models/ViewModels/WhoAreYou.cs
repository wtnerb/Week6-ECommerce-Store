using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class WhoAreYou
    {
        [Required]
        [EmailAddress]
        public string User { get; set; }

        [Required]
        [MaxLength(3)]//set the same as in CreateUser
        public string Password { get; set; }
    }
}
