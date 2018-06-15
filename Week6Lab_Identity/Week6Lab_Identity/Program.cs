using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Week6Lab_Identity.Models;

namespace Week6Lab_Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                try
                {
                    SeedAdmin.SeedDatabase(services, userManager);
                    Task prod = SeedProducts.Seed(services);

                    //Spin lock. I tried to find another solution, couldn't. This works.
                    while (prod.IsCompleted) { };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
                
                
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
