using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;

namespace Week6Lab_Identity.Models
{
    public class SeedProducts
    {
        public static async Task Seed(IServiceProvider service)
        {
            using (StoreContext context = new StoreContext(
                service.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (await context.Words.AnyAsync())
                {
                    return;
                }
                await context.Words.AddRangeAsync(Words);
                await context.SaveChangesAsync();
            }
            return;
        }

        public static List<Word> Words { get; } = new List<Word>
        {
            new Word()
            {
                Text = "unfair",
                Definition = "1. Cosequences with insufficient or disproportionate cause",
                Price = 20m,
                Usage = POS.adjective,
                Type = WordType.Education
            },
            new Word()
            {
                Text = "literally",
                Definition = "1. real and not figurative" +
                " 2. figurative and not real",
                Price = 550.3m, //Price is hiked to reduce abuse of the word
                Usage = POS.adverb
            },
            new Word()
            {
                Text = "inflammable",
                Definition = "flammable",
                Price = .30m,
                Usage = POS.adjective
            },
            new Word()
            {
                Text = "Revolution",
                Definition = "Overthrow of old ways and imposition of new, usually in a radical manner",
                Price = 3.14m,
                Usage = POS.noun,
                Type = WordType.Education
            },
            new Word()
            {
                Text = "Coup",
                Definition = "Violent overthrow of the government, usually orchestrated by military leaders",
                Price = 666m,
                Usage = POS.noun,
                Type = WordType.Education
            }
        };
    }
}
