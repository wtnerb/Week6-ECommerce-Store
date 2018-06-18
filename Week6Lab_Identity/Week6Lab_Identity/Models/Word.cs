using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Definition { get; set; }
        public POS Usage { get; set; }
        public decimal Price { get; set; }
        public WordType Type = WordType.General;
    }
}
