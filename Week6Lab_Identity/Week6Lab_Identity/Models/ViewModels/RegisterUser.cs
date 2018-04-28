using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte Age { get; set; }

        [Required]
        [DataType("Region")]
        public string Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(3, ErrorMessage = "Sorry, your password is too difficult to hack. Can you make it simpler?")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match. Did a cat walk across your keyboard?")]
        public string ConfirmPassword { get; set; }
    }
}
