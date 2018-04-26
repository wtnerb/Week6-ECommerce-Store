using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class CreateUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte Age { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal CurrentIncome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(3, ErrorMessage = "Sorry, your password is too difficult to hack. Can you make it simpler?")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match. Did a cat walk across your keyboard?")]
        public string ConfirmPassword { get; set; }
    }
}
