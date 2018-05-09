using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week6Lab_Identity.Models.ViewModels
{
    public class WordList
    {
        //TODO once db works, delete this file
        public List<Word> Words { get; set; }
        public List<Word> EduWords { get; set; }
        public WordList()
        {
            Words = new List<Word>()
                    {
                        new Word()
                        {
                            Text = "literally",
                            Definition = "1. real and not figurative" +
                            "2. figurative and not real",
                            Price = 550.3m, //Price is hiked to reduce abuse of the word
                            Usage = POS.adverb
                        },
                        new Word()
                        {
                            Text = "inflammable",
                            Definition = "flammable",
                            Price = .30m,
                            Usage = POS.adjective
                        }
                    };

            //Note: more seed data for products is in the store controller for edu data
        }
    }
}
