using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class Charge
    {
        [Required]
        [CreditCard]
        public string Number { get; set; }

        [Required]
        [Range(100, 9999)]
        public int Cvv { get; set; }

        [Required]
        [StringLength(4, MinimumLength =4)]
        public string Experation { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        //TODO Possibly insecure handling of Amount info. Refactor?
        [Required]
        public decimal Amount { get; set; }
    }
}
