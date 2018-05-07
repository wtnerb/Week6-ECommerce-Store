using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Models;
using Week6Lab_Identity.Models.ViewModels;

namespace Week6Lab_Identity.Controllers
{
    public class StoreController : Controller
    {
        [HttpGet]
        public IActionResult Prods ()
        {
            //TODO create a list of words from the db
            var words = new WordList();
            return View(words.Words);
        }

        [Authorize(Policy = "Educational")]
        [HttpGet]
        public IActionResult Student ()
        {
            //Make this data be not hard coded
            var words = new List<Word>()
            {
                new Word()
                {
                    Text = "Revolution",
                    Definition = "Overthrow of old ways and imposition of new, usually in a radical manner",
                    Price = 3.14m,
                    Usage = POS.noun
                },
                new Word()
                {
                    Text = "Coup",
                    Definition = "Violent overthrow of the government, usually orchestrated by military leaders",
                    Price = 666m,
                    Usage = POS.noun
                }
            };
            return View(words);

        }
    }
}
